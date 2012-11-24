﻿using System;
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
    public sealed partial class OperacionesSimples : Matrices.Common.LayoutAwarePage
    {

        private int tamano;

        public OperacionesSimples()
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string parametros = e.Parameter as string;

            tamano = int.Parse(parametros);
          
            iniciarMatriz(stack1, 1);
            iniciarMatriz(stack2, 2);
        }

        private void iniciarMatriz(StackPanel auxStack, int auxMatrix)
        {

            int count = 0;

            for (int i = 0; i < tamano; i++)
            {
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;


                for (int j = 0; j < tamano; j++)
                {
                    TextBox cajaTexto = new TextBox();
                    cajaTexto.Height = 80;
                    cajaTexto.Width = 80;
                    cajaTexto.Name = "C" + count + "M" + auxMatrix;
                    cajaTexto.Text = "1";
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
                auxStack.VerticalAlignment = VerticalAlignment.Center;
                auxStack.Children.Add(stack);
            }

        }//Fin de iniciarMatriz

        private void calcularTraspuesta(object sender, RoutedEventArgs e)
        {
            try
            {
                ponerDatos(Codigo.Operaciones.traspuesta(sacarDatos(), tamano));
                anuncioResultado.Text = "Transpuesta";
            }
            catch (Exception ex)
            {
                mostrarMEnsaje();
            }
        }

        private void calcularInversa(object sender, RoutedEventArgs e)
        {

            try
            {
                Codigo.Matrix matrizInversa = new Codigo.Matrix(sacarDatosDecimal());
                ponerDatosDecimal(matrizInversa.Inversa());
                anuncioResultado.Text = "Inversa";
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
                    TextBox cajaAux = stack1.FindName(nombre) as TextBox;
                    System.Diagnostics.Debug.WriteLine("el numero es= " + cajaAux.Text);

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
                    TextBox cajaAux = stack1.FindName(nombre) as TextBox;
                    System.Diagnostics.Debug.WriteLine("el numero es= " + cajaAux.Text);

                    matrizADevolver[i, j] = decimal.Parse(cajaAux.Text);
                    //matrizADevolver[i, j] = 1;

                    contador++;
                }

            return matrizADevolver;
        }//Fin de sacar datos

        private void ponerDatos(float[,] matriz)
        {
            string nombre;
            int contador = 0;

            for (int i = 0; i < tamano; i++)
                for (int j = 0; j < tamano; j++)
                {
                    nombre = "C" + contador + "M2";
                    TextBox cajaAux = stack1.FindName(nombre) as TextBox;
                    cajaAux.Text = matriz[i, j].ToString();
                    contador++;
                }

        }//Fin de PonerDatos

        private void ponerDatosDecimal(decimal[,] matriz)
        {
            string nombre;
            int contador = 0;

            for (int i = 0; i < tamano; i++)
                for (int j = 0; j < tamano; j++)
                {
                    nombre = "C" + contador + "M2";
                    TextBox cajaAux = stack1.FindName(nombre) as TextBox;
                    cajaAux.Text = matriz[i, j].ToString();
                    contador++;
                }

        }//Fin de PonerDatos
      
    }
}
