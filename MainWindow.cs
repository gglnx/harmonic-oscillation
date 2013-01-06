using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace HarmonicOscillation
{
    /**
     * Diese Klasse enthält die Logik für das Hauptfenster
     */
    public partial class MainWindow : Form
    {
        /**
         * Speichert, ob zurzeit ein Eingabefehler vorliegt
         * Array: textBox1, textBox2, textBox3
         */
        private bool[] hasError = {false, false, false};

        /**
         * Klasse zur Berechnung
         */
        private Calculation cal = new Calculation();

        /**
         * Konstruktor
         */
        public MainWindow()
        {
            // Formular generieren
            InitializeComponent();

            // Standardwerte
            cal.PhaseAngle = (Math.PI / 180) * 45D;
            cal.AngularFrequency = 2 * Math.PI * 2D;
            cal.Amplitude = 4D;
        }

        /**
         * Diese Funktion prüft, ob der Pharsenwinkel korrekt eingegeben ist
         */
        private void validatePhaseAngle(object sender, EventArgs e)
        {
            // Pharsenwinkel deklarieren
            int phaseangle;

            // Feld zurücksetzen
            resetError(0, label1, textBox1);

            // Eingegebenen Wert holen
            string input = textBox1.Text;

            // Prüfen, ob der Eingabewert eine Zahl (int) ist
            try {
                // Eingabewert in eine Zahl konvertieren
                phaseangle = Convert.ToInt32(input);
            } catch {
                // Fehlermeldung anzeigen und Funktion abbrechen
                displayError(0, label1, textBox1, "Der Pharsenwinkel muss eine Zahl sein.");
                return;
            }

            // Prüfen ob der Zahlenwert zwischen 0 und 360 liegt
            if (phaseangle < 0 || phaseangle > 360)
            {
                // Fehlermeldung anzeigen und Funktion abbrechen
                displayError(0, label1, textBox1, "Der Pharsenwinkel muss zwischen 0° und 360° liegen.");
                return;
            }

            // Grad in Bogenmaß umrechnen und der Berechnungsklasse übergeben
            cal.PhaseAngle = (Math.PI / 180) * phaseangle;
        }

        /**
         * Diese Funktion prüft, ob die Schwingungsfrequenz korrekt eingegeben ist
         */
        private void validateAngularFrequency(object sender, EventArgs e)
        {
            // Schwingungsfrequenz deklarieren
            double angularfrequency;

            // Feld zurücksetzen
            resetError(1, label2, textBox2);

            // Eingegebenen Wert holen
            string input = textBox2.Text;

            // Prüfen, ob der Eingabewert eine Zahl (double) ist
            try
            {
                // Eingabewert in eine Zahl konvertieren
                angularfrequency = Convert.ToDouble(input);
            }
            catch
            {
                // Fehlermeldung anzeigen und Funktion abbrechen
                displayError(1, label2, textBox2, "Die Schwingungsfrequenz muss eine Zahl sein.");
                return;
            }

            // Der Berechnungsklasse übergeben
            cal.AngularFrequency = 2 * Math.PI * angularfrequency;
        }

        /**
         * Diese Funktion prüft, ob der Scheitelwert / Amplitude korrekt eingegeben ist
         */
        private void validateAmplitude(object sender, EventArgs e)
        {
            // Scheitelwert deklarieren
            double amplitude;

            // Feld zurücksetzen
            resetError(2, label3, textBox3);

            // Eingegebenen Wert holen
            string input = textBox3.Text;

            // Prüfen, ob der Eingabewert eine Zahl (double) ist
            try
            {
                // Eingabewert in eine Zahl konvertieren
                amplitude = Convert.ToDouble(input);
            }
            catch
            {
                // Fehlermeldung anzeigen und Funktion abbrechen
                displayError(2, label3, textBox3, "Der Scheitelwert muss eine Zahl sein.");
                return;
            }

            // Der Berechnungsklasse übergeben
            cal.Amplitude = amplitude;
        }

        /**
         * Diese Funktion setzt Label und Input-Feld auf das Standard-Design
         * zurück
         */
        private void resetError(int field, Label label, TextBox input) {
            // Status resetten
            hasError[field] = false;
            
            // Textfarbe vom Label und Input-Feld auf Textfarbe des Systems (schwarz) ändern
            label.ForeColor = System.Drawing.SystemColors.WindowText;
            input.ForeColor = System.Drawing.SystemColors.WindowText;

            // Hintergrundfarbe vom Input-Feld auf Systemstandard ändern
            input.BackColor = System.Drawing.SystemColors.Window;

            // Verstecke die Fehlermeldungsanzeige
            label5.Visible = false;

            // Buttons aktivieren, wenn in allen drei Feldern kein Eingabefehler mehr vorliegt
            if (!hasError[0] && !hasError[1] && !hasError[2])
                button1.Enabled = button2.Enabled = true;
        }

        /**
         * Diese Funktion zeigt dem Benutzer Fehlermeldungen an, zusätzlich
         * werden Label und Input-Feld rot markiert
         */
        private void displayError(int field, Label label, TextBox input, string message) { 
            // Status setzen, dass zurzeit ein Eingabefehler vorliegt
            hasError[field] = true;
            
            // Textfarbe vom Label und Input-Feld auf rot ändern
            label.ForeColor = System.Drawing.Color.Red;
            input.ForeColor = System.Drawing.Color.Red;

            // Hintergrundfarbe vom Input-Feld auf rosa (MistyRose) ändern
            input.BackColor = System.Drawing.Color.MistyRose;

            // Fehlermeldung setzen und anzeigen
            label5.Text = message;
            label5.Visible = true;

            // Buttons deaktivieren
            button1.Enabled = button2.Enabled = false;
        }

        /**
         * Diese Funktion stellt die drei Gesetze als Graphen in einem neuen Fenster dar
         */
        private void showGraph(object sender, EventArgs e)
        {
            // Fenster für den Graph laden
            Graph graph = new Graph();

            // Titelzeile setzen
            graph.Text = "Graph: φ = " + textBox1.Text + "°, f = " + textBox2.Text + ", ymax = " + textBox3.Text;

            // Graphen vorbereiten
            Series way = new Series("Zeit-Weg");
            Series speed = new Series("Zeit-Geschwindigkeit");
            Series acceleration = new Series("Zeit-Beschleunigung");

            // Darstellung einstellen
            way.ChartType = speed.ChartType = acceleration.ChartType = SeriesChartType.Spline;

            // Graph zeichnen
            for (double i = -4; i <= 4; i += 0.01)
            {
                way.Points.AddXY(i, cal.calculateWay(i));
                speed.Points.AddXY(i, cal.calculateSpeed(i));
                acceleration.Points.AddXY(i, cal.calculateAcceleration(i));
            }

            // Graphen hinzufügen
            graph.addGraphWay(way);
            graph.addGraphSpeed(speed);
            graph.addGraphAcceleration(acceleration);

            // Fenster öffnen
            graph.Show();
        }

        /**
         * Diese Funktion stellt die drei Gesetze als Wertetabelle in einem neuen Fenster dar
         */
        private void showDatatable(object sender, EventArgs e)
        {
            // Fenster für die Wertetabelle laden
            Datatable datatable = new Datatable();

            // Titelzeile setzen
            datatable.Text = "Wertetabelle: φ = " + textBox1.Text + "°, f = " + textBox2.Text + ", ymax = " + textBox3.Text;

            // Berechnung übergeben
            datatable.setCalculation(cal);

            // Wertetabelle ausfüllen
            for (double i = -1; i <= 1; i += 0.05)
            {
                datatable.addRow(Math.Round(i, 2), cal.calculateWay(i), cal.calculateSpeed(i), cal.calculateAcceleration(i));
            }

            // Fenster öffnen
            datatable.Show();
        }

        private void MainWindow_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            // Hilfe-Fenster anzeigen
            HelpWindow help = new HelpWindow();
            help.Show();
        }
    }
}
