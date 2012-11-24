using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Matrices
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Resultado : Matrices.Common.LayoutAwarePage
    {

        private int tamano;

        public Resultado()
        {
            System.Diagnostics.Debug.WriteLine("En constructor");
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            float[,] matrizResultado = e.Parameter as float[,];
            int tam = matrizResultado.Length;
            System.Diagnostics.Debug.WriteLine("El tamaño es:" + tam);
            tamano = (int)Math.Sqrt(tam);
            iniciarMatriz(stackRes, 1, matrizResultado);
            calcularTraza();
            cacularDeterminate();


        }//Fin de onNavigateTo

        private void iniciarMatriz(StackPanel auxStack, int auxMatrix, float[,] res)
        {

            int tam = tamano;
            string linea = "";
            int count = 0;

            for (int i = 0; i < tam; i++)
            {
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;

                linea = "";

                for (int j = 0; j < tam; j++)
                {
                    TextBox cajaTexto = new TextBox();
                    cajaTexto.Height = 90;
                    cajaTexto.Width = 90;
                    cajaTexto.Name = "C" + count + "M" + auxMatrix;
                    cajaTexto.Text = res[i, j].ToString();
                    linea += " " + res[i, j] + " ";
                    cajaTexto.IsReadOnly = false;
                    cajaTexto.InputScope = new InputScope
                    {
                        Names ={ new InputScopeName() { NameValue=InputScopeNameValue.Number}
                        }
                    };

                    cajaTexto.TextAlignment = TextAlignment.Center;
                    cajaTexto.Margin = new Thickness(0, 0, 5, 5);

                    count++;

                    stack.Children.Add(cajaTexto);
                }
                stack.HorizontalAlignment = HorizontalAlignment.Center;
                auxStack.Children.Add(stack);
                auxStack.VerticalAlignment = VerticalAlignment.Center;

                System.Diagnostics.Debug.WriteLine(linea);
            }

        }//Fin de iniciarMatriz

        private void cacularDeterminate()
        {
            try
            {
                Codigo.Matrix matrizDet = new Codigo.Matrix(sacarDatosDecimal());
                decimal determinate = matrizDet.Determinante();
                anuncioDeterminante.Text = determinate.ToString();
            }
            catch (Exception ex)
            {
                mostrarMEnsaje();
            }
        }

        private void calcularTraza()
        {
            try
            {

                float traza = Codigo.Operaciones.traza(sacarDatos(), tamano);
                anuncioTraza.Text = traza.ToString();
            }
            catch (Exception ex)
            {
                mostrarMEnsaje();
            }
        }

        private async void mostrarMEnsaje()
        {
            var messageDialog = new MessageDialog("Hubo algun error al hacer el calculo");
            await messageDialog.ShowAsync();
        }

        private float[,] sacarDatos()
        {
            float[,] matrizADevolver = new float[tamano, tamano];
            string nombre;
            int contador = 0;

            for (int i = 0; i < tamano; i++)
                for (int j = 0; j < tamano; j++)
                {
                    nombre = "C" + contador + "M1";
                    TextBox cajaAux = stackRes.FindName(nombre) as TextBox;
                    

                    matrizADevolver[i, j] = float.Parse(cajaAux.Text);
                    //matrizADevolver[i, j] = 1;

                    contador++;
                }

            return matrizADevolver;
        }//Fin de sacar datos

        private decimal[,] sacarDatosDecimal()
        {
            decimal[,] matrizADevolver = new decimal[tamano, tamano];
            string nombre;
            int contador = 0;

            for (int i = 0; i < tamano; i++)
                for (int j = 0; j < tamano; j++)
                {
                    nombre = "C" + contador + "M1";
                    TextBox cajaAux = stackRes.FindName(nombre) as TextBox;

                    matrizADevolver[i, j] = decimal.Parse(cajaAux.Text);
                    //matrizADevolver[i, j] = 1;

                    contador++;
                }

            return matrizADevolver;
        }//Fin de sacar datos
    }
}
