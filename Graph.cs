using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace HarmonicOscillation
{
    /**
     * Diese Klasse enthält die Logik für das Graph-Fenster
     */
    public partial class Graph : Form
    {
        /**
         * Konstruktor
         */
        public Graph()
        {
            InitializeComponent();
        }

        /**
         * Neuen Datenpunk im Weg-Diagramm anlegen
         */
        public void addGraphWay(Series graph) {
            chart1.Series.Add(graph);
        }

        /**
         * Neuen Datenpunk im Geschwindigkeit-Diagramm anlegen
         */
        public void addGraphSpeed(Series graph)
        {
            chart2.Series.Add(graph);
        }

        /**
         * Neuen Datenpunk im Beschleunigung-Diagramm anlegen
         */
        public void addGraphAcceleration(Series graph)
        {
            chart3.Series.Add(graph);
        }
    }
}
