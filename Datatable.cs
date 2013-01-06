using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HarmonicOscillation
{
    /**
     * Diese Klasse enthält die Logik für das Wertetabellen-Fenster
     */
    public partial class Datatable : Form
    {
        /**
         * Klasse zur Berechnung mit Werten
         */
        private Calculation cal;

        /**
         * Konstruktor
         */
        public Datatable()
        {
            InitializeComponent();
        }

        /**
         * Berechnung eintragen
         */
        public void setCalculation(Calculation c)
        {
            cal = c;
        }

        /**
         * Neue Zeile hinzufügen
         */
        public void addRow(double x, double c1, double c2, double c3) {
            dataGridView1.Rows.Add(x.ToString(), c1.ToString(), c2.ToString(), c3.ToString());
        }
    }
}
