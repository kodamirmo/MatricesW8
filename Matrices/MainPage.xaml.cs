using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Matrices
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class BasicPage1 : Matrices.Common.LayoutAwarePage
    {
        public BasicPage1()
        {
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

        private void checkRadio(object sender, RoutedEventArgs e)
        {
            RadioButton objeto = sender as RadioButton;

            if (objeto == RBSuma)
            {
                TBoperacion.Text = "Suma";
            }
            else if (objeto == RBResta)
            {
                TBoperacion.Text = "Resta";
            }
            else if (objeto == RBMultiplicacion)
            {
                TBoperacion.Text = "Multiplicacion";
            }
            else if (objeto == RBInversa)
            {
                TBoperacion.Text = "Inversa";
            }
            else if (objeto == RBTraspuesta)
            {
                TBoperacion.Text = "Traspuesta";
            }
            else if (objeto == RBDeterminante)
            {
                TBoperacion.Text = "Determinante";
            }
            else if (objeto == RBTraza)
            {
                TBoperacion.Text = "Traza";
            }
            else if (objeto == RBSistema)
            {
                TBoperacion.Text = "Sistema de Ecuaciones";

            }
            
        }

        private async void cambiaTexto(object sender, TextChangedEventArgs e)
        {
            TextBox caja = sender as TextBox;

            if (true==RBSistema.IsChecked)
            {
                var messageDialog = new MessageDialog("Esta opcion esta deshabilitada en el Sistema de Ecuaciones");
                await messageDialog.ShowAsync();

                TBtam2.Text = TBtam1.Text = "N";
            }

            if (caja == TBtam1)
            {
                TBtam2.Text = TBtam1.Text;
            }
            else
            {
                TBtam1.Text = TBtam2.Text;
            }
        }

        private bool checarNumero()
        {
            
            int numero;

            try
            {
                numero=int.Parse( TBtam2.Text);
            }
            catch (Exception e) 
            {
                return false;
            }

            if (numero < 2 || numero > 4)
                return false;

            return true;
            
        }

        private async void irACalculos(object sender, RoutedEventArgs e)
        {

            Object[] parametrros = new Object[2];
            string numero = TBtam2.Text;

            if (!checarNumero())
            {
                var messageDialog = new MessageDialog("Solo son validos numeros entre 0 y 4");
                await messageDialog.ShowAsync();

                return;
            }
            if (RBSuma.IsChecked==true)
            {
                parametrros[0] = numero;
                parametrros[1] = 0;

                if (this.Frame != null)
                {
                    this.Frame.Navigate(typeof(OperacionesDobles),parametrros);
                }
            }
            else if (RBResta.IsChecked == true)
            {
                parametrros[0] = numero;
                parametrros[1] = 1;

                if (this.Frame != null)
                {
                    this.Frame.Navigate(typeof(OperacionesDobles), parametrros);
                }
            }
            else if (RBMultiplicacion.IsChecked == true)
            {
                parametrros[0] = numero;
                parametrros[1] = 2;

                if (this.Frame != null)
                {
                    this.Frame.Navigate(typeof(OperacionesDobles), parametrros);
                }
            }

            else if (RBInversa.IsChecked == true)
            {

                if (this.Frame != null)
                {
                    
                    this.Frame.Navigate(typeof(OperacionesSimples), numero);
                }
            }
            else if (RBTraspuesta.IsChecked == true)
            {
            
                if (this.Frame != null)
                {
                    this.Frame.Navigate(typeof(OperacionesSimples), numero);
                }
            }
            else if (RBTraza.IsChecked == true)
            {

                if (this.Frame != null)
                {

                    this.Frame.Navigate(typeof(OperacionesUnarias), numero);
                }
            }
            else if (RBDeterminante.IsChecked == true)
            {

                if (this.Frame != null)
                {
                    this.Frame.Navigate(typeof(OperacionesUnarias), numero);
                }
            }
            else if (RBSistema.IsChecked == true)
            {

                if (this.Frame != null)
                {
                    this.Frame.Navigate(typeof(SistemaDeEcuaciones));

                }
            }
            else
            {
                var messageDialog = new MessageDialog("Seleccione alguna operacion");
                await messageDialog.ShowAsync();
            }
        }
    }
}
