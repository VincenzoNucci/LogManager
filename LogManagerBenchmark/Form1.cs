using LiveCharts;
using LiveCharts.Wpf;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Media;
using LogManager;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using Trace = LogManager.Trace;

namespace LogManagerBenchmark
{
    public partial class Form1 : Form
    {
        private static bool isConnected = false;
        private static MongoClient client = null;
        

        public Form1()
        {
            InitializeComponent();
            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> {4, 6, 5, 2, 7}
                },
                new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> {6, 7, 3, 4, 6},
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> {5, 2, 8, 3},
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15
                }
            };

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Logs (n)"
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Time (ms)",
                LabelFormatter = value => value.ToString("C")
            });

            cartesianChart1.LegendLocation = LegendLocation.Right;

            //modifying the series collection will animate and update the chart
            cartesianChart1.Series.Add(new LineSeries
            {
                Values = new ChartValues<double> { 5, 3, 2, 4, 5 },
                LineSmoothness = 0, //straight lines, 1 really smooth lines
                PointGeometry = Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
                PointGeometrySize = 50,
                PointForeground = System.Windows.Media.Brushes.Gray
            });
        }



        private void btnConnection_Click(object sender, EventArgs e)
        {
            if (txtConnectionString.Text.Length == 0)
                return;

            client = new MongoClient("mongodb://" + txtConnectionString.Text);
            {
                if (client != null)
                {
                    try
                    {
                        lblStatus.Text = "Connecting...";
                        //var database = client.GetDatabase("admin");
                        Thread t = new Thread(() => {
                            try
                            {
                                var database = client.ListDatabases();
                                database.MoveNext();
                                if (client.Cluster.Description.State == MongoDB.Driver.Core.Clusters.ClusterState.Connected)
                                {
                                    isConnected = true;
                                }
                                else
                                    isConnected = false;
                            }catch (Exception)
                            {
                                isConnected = false;
                            }                                                    
                        });
                        t.Start();
                        

                    }
                    catch (Exception)
                    {

                        lblStatus.Text = "Unable to connect, check connection";
                    }

                    if (isConnected)
                    {
                        lblStatus.Text = "Connected to " + txtConnectionString.Text;
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                        lblDatabase.Visible = true;
                        lblCollection.Visible = true;
                        cBoxDatabase.Visible = true;
                        cBoxCollection.Visible = true;

                        using (var cursor = client.ListDatabaseNamesAsync())
                        {
                            var dbListNames = cursor.Result.ToList<string>();
                            for (var i = 0; i < dbListNames.Count; i++)
                            {
                                cBoxDatabase.Items.Add(dbListNames[i]);
                            }
                        }
                    }


                    else
                    {
                        lblStatus.Text = "Error connecting to MongoDB";
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                        lblDatabase.Visible = false;
                        lblCollection.Visible = false;
                        cBoxDatabase.Visible = false;
                        cBoxCollection.Visible = false;
                        cBoxDatabase.Items.Clear();
                        cBoxCollection.Items.Clear();
                    }
                }
            }
        }

        private void cBoxDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            cBoxCollection.Items.Clear();
            
                using (var cursor = client.GetDatabase(cBoxDatabase.SelectedItem.ToString()).ListCollectionNamesAsync())
            {
                var dbListNames = cursor.Result.ToList<string>();
                for (var i = 0; i < dbListNames.Count; i++)
                {
                    cBoxCollection.Items.Add(dbListNames[i]);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            bool traceSelected = false;

            switch(cbxTraceClass.SelectedIndex)
            {
                case 0: //Trace class
                    Trace.Buffers = (int)nudBuffers.Value;
                    Trace.BufferSize = (int)nudBufferSize.Value;
                    Trace.Connect(txtConnectionString.Text, cBoxDatabase.Text, cBoxCollection.Text);
                    traceSelected = true;
                break;

                case 1: //Concurrent trace class
                    ConcurrentTrace.NumberOfBuffers = (int)nudBuffers.Value;
                    ConcurrentTrace.BufferSize = (int)nudBufferSize.Value;
                    ConcurrentTrace.Connect(txtConnectionString.Text, cBoxDatabase.Text, cBoxCollection.Text);
                    traceSelected = true;
                break;

                case 2: //Timed concurrent trace class
                    MessageBox.Show("Not implemented");
                    traceSelected = false;
                    break;

                default:
                    MessageBox.Show("Error in executing the test");
                    traceSelected = false;
                    break;
            }

            if (!traceSelected)
                return;

            List<Task> tasks = new List<Task>();
            for (int i = 0; i < nudThreads.Value; i++)
            {
                tasks.Add(Task.Factory.StartNew(Print));
            }
        }

        private void Print()
        {
            List<string> stringhe = new List<string>() {"Ciao", "Come va?", "Tutto bene", "Random text", "Oh bè", "Non so", "Mmmmmh",
            "asdasd" ,"MongoDB", "Test", "boh boh"};

            Random r = new Random();
            int rand = r.Next(stringhe.Count);

            Log l = new Log(LogLevel.DEBUG, DateTime.Now, new Origin(Thread.CurrentThread.ManagedThreadId), stringhe[rand]);
            Stopwatch stopw = new Stopwatch();
            stopw.Start();
            
            ConcurrentTrace.Write(l);

            stopw.Stop();
            console.AppendText($"{Task.CurrentId} -> {stopw.ElapsedMilliseconds}");

        }
    }
}
