using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TestRegExp
{
    public partial class FormMain : Form
    {
        #region Records
        /// <summary>
        /// Risultato della ricerca numerica
        /// </summary>
        /// <param name="Number">Numero</param>
        /// <param name="Index">Indice</param>
        public record NumberResult(double Number, int Index);
        /// <summary>
        /// Risultato della ricerca testuale
        /// </summary>
        /// <param name="Word">Parola</param>
        /// <param name="Index">Indice</param>
        /// <param name="Distance">Distanza di Levenshtein</param>
        public record TextResult(string Word, int Index, int Distance);
        #endregion
        #region Fields
        /// <summary>
        /// Separatori di parole
        /// </summary>
        private static readonly char[] separator = [' ', ',', '!', '.'];
        #endregion
        #region Methods
        /// <summary>
        /// Costruttore di default
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Calcola la distanza Levenshtein tra due parole
        /// </summary>
        /// <param name="word">Parola reale</param>
        /// <param name="sample">Parola campione</param>
        /// <returns>La distanza Levenshtein</returns>
        public static int ComputeLevenshteinDistance(string word, string sample)
        {
            // Inizializzazioni
            int n = word.Length;
            int m = sample.Length;
            int[,] d = new int[n + 1, m + 1];
            // Verifica se le due parole sono vuote
            if (n == 0)
                return m;
            if (m == 0)
                return n;
            // Inizializza la matrice
            for (int i = 0; i <= n; d[i, 0] = i++) { }
            for (int j = 0; j <= m; d[0, j] = j++) { }
            // Calcola la distanza Levenshtein
            for (int i = 1; i <= n; i++) {
                for (int j = 1; j <= m; j++) {
                    int cost = (sample[j - 1] == word[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Ritorna la distanza Levenshtein
            return d[n, m];
        }
        /// <summary>
        /// Estrae i numeri presenti nella frase e le loro indici
        /// </summary>
        /// <param name="sentence">La frase</param>
        /// <returns>La lista di risultati ed indici</returns>
        public static List<NumberResult> ComputeNumbers(string sentence)
        {
            // RegEx per la ricerca dei numeri
            var numbersWithIndices = new List<NumberResult>();
            var matches = GetNumbersRegEx().Matches(sentence);
            // Estrae i numeri presenti nella frase
            foreach (Match match in matches) {
                if (double.TryParse(match.Value, out double number))
                    numbersWithIndices.Add(new NumberResult(number, match.Index));
            }
            // Ritorna la lista dei numeri con i rispettivi indici
            return numbersWithIndices;
        }
        /// <summary>
        /// Calcola gli indici e le distanze Levenshtein per la parola indicata all'interno di una frase.
        /// </summary>
        /// <param name="sentence">Frase contenente i termini</param>
        /// <param name="sample">Parola campione</param>
        /// <returns>La lista di parole, indici e distanze</returns>
        public static List<TextResult> ComputeWordDistances(string sentence, string sample)
        {
            // Lista di risultati
            var results = new List<TextResult>();
            int currentPosition = 0;
            // Divide la frase in parole
            foreach (var word in sentence.Split(separator, StringSplitOptions.RemoveEmptyEntries)) {
                // Cerca la parola all'interno della frase partendo dalla posizione corrente
                while ((currentPosition = sentence.IndexOf(word, currentPosition)) != -1) {
                    // Calcola la distanza Levenshtein tra la parola e la parola campione
                    int dist = ComputeLevenshteinDistance(word, sample);
                    // Aggiunge il risultato alla lista
                    results.Add(new TextResult(word, currentPosition, dist));
                    // Avanza nella frase per la prossima parola
                    currentPosition += word.Length;
                }
                // Reset per la prossima parola
                currentPosition = 0;
            }
            // Ritorna la lista di risultati
            return results;
        }
        /// <summary>
        /// Restituisce un'espressione regolare per ricercare i numeri nella frase
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex(@"\d+")]
        private static partial Regex GetNumbersRegEx();
        /// <summary>
        /// Evento di testo modificato
        /// </summary>
        /// <param name="sender">Controllo</param>
        /// <param name="e">Argomenti</param>
        private void TextBoxText_TextChanged(object sender, EventArgs e)
        {
            try {
                var tb = (TextBox)sender;
                // Cerca la parola cassetto nella frase
                var results = ComputeWordDistances(tb.Text, "cassetto");
                // Riordina i risultati a seconda delle distanze
                var sortedResults = results.OrderBy(r => r.Distance).ToList();
                var numericResults = ComputeNumbers(tb.Text);
                var sortedNumerics = numericResults.Where(r => r.Index > sortedResults[0].Index).OrderBy(r => r.Index).ToList();
                // Verifica se ci sono risultati numerici
                if (sortedResults.Count != 0 && sortedNumerics.Count != 0) {
                    // Stampa il risultato migliore
                    labelOutput.Text = $"Parola: {sortedResults[0].Word}, Indice: {sortedResults[0].Index}, Distanza Levenshtein: {sortedResults[0].Distance}, Numero: {sortedNumerics[0].Number}";
                }
                else {
                    // Stampa un messaggio di avviso se non ci sono risultati numerici
                    labelOutput.Text = "Nessun cassetto presente nella frase.";
                }
            }
            catch (Exception exc) {
                Trace.WriteLine(exc.ToString());
            }
        }
        #endregion
    }
}
