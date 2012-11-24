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
    public sealed partial class SistemaDeEcuaciones : Matrices.Common.LayoutAwarePage
    {
        private int tamano = 3;

        public SistemaDeEcuaciones()
        {
            this.InitializeComponent();
            inicarStackIguales();
            iniciarsistema();
            iniciarVector();
            inicarStackRespuestas();
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

        private void inicarStackRespuestas()
        {
            for (int i = 0; i < tamano; i++)
            {
                TextBlock cajaAux = new TextBlock();
                cajaAux.Width = 200;
                cajaAux.Height = 40;
                cajaAux.Name = "X" + i;
                cajaAux.Text = "X" + i + " = ?";
                cajaAux.Margin = new Thickness(0, 10, 0, 0);
                cajaAux.FontSize = 36;
                stackRespuestas.Children.Add(cajaAux);
            }

            stackRespuestas.VerticalAlignment = VerticalAlignment.Center;
            stackRespuestas.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private void inicarStackIguales()
        {
            for (int i = 0; i < tamano; i++)
            {
                TextBlock cajaAux = new TextBlock();
                cajaAux.Width = 100;
                cajaAux.Height = 40;
                cajaAux.Text = "=";
                cajaAux.Margin = new Thickness(10, 10, 0, 0);
                cajaAux.FontSize = 36;
                stackIgual.Children.Add(cajaAux);
            }

            stackIgual.VerticalAlignment = VerticalAlignment.Center;
            stackIgual.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private void iniciarVector()
        {
            for (int i = 0; i < tamano; i++)
            {
                TextBox cajaAux = new TextBox();
                cajaAux.Width = 50;
                cajaAux.Height = 40;
                cajaAux.Name = "V" + i;
                cajaAux.Text = "";
                cajaAux.Margin = new Thickness(0, 10, 0, 0);
                
                stackVector.Children.Add(cajaAux);
            }

            stackVector.VerticalAlignment = VerticalAlignment.Center;
            stackVector.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private void iniciarsistema()
        {
            int count = 0;

            for (int i = 0; i < tamano; i++)
            {
                StackPanel auxStack = new StackPanel();
                auxStack.Orientation = Orientation.Horizontal;

                for (int j = 0; j < tamano; j++)
                {
                    TextBox cajaAux = new TextBox();
                    cajaAux.Width = 75;
                    cajaAux.Height = 40;
                    cajaAux.Name = "C" + count;

                    TextBlock bloque = new TextBlock();
                    bloque.Width = 100;
                    bloque.Height = 40;
                    bloque.Margin = new Thickness(10, 10, 0, 0);
                    bloque.FontSize = 26;
                    if(j==(tamano-1))
                        bloque.Text = " X" + j + "  ";
                    else
                        bloque.Text = " X" + j + "   + ";

                    auxStack.Children.Add(cajaAux);
                    auxStack.Children.Add(bloque);

                    count++;
                }

                stackSistema.Children.Add(auxStack);
            }

            stackSistema.VerticalAlignment = VerticalAlignment.Center;
            stackSistema.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private decimal[,] sacarSistema()
        {
            decimal[,] sistema = new decimal[tamano, tamano];

            int count = 0;

            System.Diagnostics.Debug.WriteLine("En Sacando sistema\n");

            for (int i = 0; i < tamano; i++)
            {
                for (int j = 0; j < tamano; j++)
                {
                    TextBox caja = stackSistema.FindName("C" + count) as TextBox;
                    sistema[i, j] = decimal.Parse(caja.Text);
                    System.Diagnostics.Debug.WriteLine("" + sistema[i,j] + " ");
                    count++;
                }
                System.Diagnostics.Debug.WriteLine( "\n");
            }
            return sistema;
        }

        private decimal[] sacarVecror()
        {
            decimal[] vector = new decimal[tamano];

            System.Diagnostics.Debug.WriteLine("En Sacando vector\n");

            for (int i = 0; i < tamano; i++)
            {
                TextBox caja = stackVector.FindName("V" + i) as TextBox;
                vector[i] = decimal.Parse(caja.Text);
                System.Diagnostics.Debug.WriteLine("Valor " + i + " es " + vector[i] + "\n");
            }

            return vector;
        }

        private void calcularSistema(object sender, RoutedEventArgs e)
        {

            try
            {
                decimal[,] array = sacarSistema();
                decimal[] res = sacarVecror();
                Codigo.Matrix matriz = new Codigo.Matrix(array);
                decimal[] res2 = matriz.Cramer(res);
                presentarDatos(res2);
            }
            catch(Exception exc)
            {
                mostrarMEnsaje();
            }
        }

        private async void mostrarMEnsaje(){
            var messageDialog = new MessageDialog("Hubo algun error al hacer el calculo");
            await messageDialog.ShowAsync();
        }

        private void presentarDatos(decimal[] res2)
        {
            System.Diagnostics.Debug.WriteLine("Respuesta es" + res2[0] + " " + res2[1] + " " + res2[2]);
            for (int i = 0; i < tamano; i++)
            {
                TextBlock caja = stackRespuestas.FindName("X" + i) as TextBlock;
                caja.Text = "X" + i + " = " + res2[i].ToString();
            }
        }
    }
}
