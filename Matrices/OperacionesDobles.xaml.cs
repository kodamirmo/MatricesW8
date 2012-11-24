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
    public sealed partial class OperacionesDobles : Matrices.Common.LayoutAwarePage
    {

        private int tamano;

        public OperacionesDobles()
        {
            this.InitializeComponent();
            
        }

        private void iniciar()
        {
            iniciarMatriz(stack1, 1);
            iniciarMatriz(stack2, 2);
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
            Object[] parametros = e.Parameter as Object[];
            tamano = int.Parse((string)parametros[0]);

            System.Diagnostics.Debug.WriteLine("EL  tamaño es: " + parametros[0]);
            System.Diagnostics.Debug.WriteLine("EL  indice es: " + parametros[1]);

            Operaciones.SelectedIndex = (int)parametros[1];
            iniciar();
        }

        private void iniciarMatriz(StackPanel auxStack,int auxMatrix)
        {
            
            int count = 0;

            for (int i = 0; i < tamano; i++)
            {
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;


                for (int j = 0; j < tamano; j++)
                {
                    TextBox cajaTexto = new TextBox();
                    cajaTexto.Height = 90;
                    cajaTexto.Width = 90;
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
                auxStack.Children.Add(stack);
                auxStack.VerticalAlignment = VerticalAlignment.Center;
                
            }
          
        }//Fin de iniciarMatriz

        private void Calcular_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (this.Frame != null)
                {
                    this.Frame.Navigate(typeof(Resultado), hacerCalulo());
                }
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

        private float[,] hacerCalulo()
        {
            float[,] matriz1 = sacarDatos(1);
            float[,] matriz2 = sacarDatos(2);
            float[,] matrizResultado = operar(Operaciones.SelectedIndex,matriz1,matriz2);

            return matrizResultado;
        }

        private float[,] sacarDatos(int matriz)
        {
            float[,] matrizADevolver = new float[tamano, tamano];
            string nombre;
            int contador = 0;
            string linea="";

            for (int i = 0; i < tamano; i++)
            {
                linea = "";
                for (int j = 0; j < tamano; j++)
                {
                    nombre = "C" + contador + "M" + matriz;
                    TextBox cajaAux = stack1.FindName(nombre) as TextBox;
                    //System.Diagnostics.Debug.WriteLine("el numero es= " + cajaAux.Text);
                    linea += " " + cajaAux.Text + " ";

                    // matrizADevolver[i, j] = float.Parse(cajaAux.Text);
                    matrizADevolver[i, j] = float.Parse(cajaAux.Text);

                    contador++;
                }
                System.Diagnostics.Debug.WriteLine(linea);
            }

            System.Diagnostics.Debug.WriteLine(" ");

            return matrizADevolver;
        }//Fin de sacar datos

        private float[,] operar(int op, float[,] m1, float[,] m2) 
        {
            float[,] res=null;

            switch (op)
            {
                case 0: 
                    res=sumar(m1,m2);
                    break;
                 
                case 1:
                    res=restar(m1, m2);
                    break;
                    
                case 2:
                    res=multiplicar(m1, m2);
                    break;

                default:
                    break;
            }

            return res;
        }

        private float[,] sumar(float[,] m1, float[,] m2)
        {
            float[,] respuesta = new float[tamano, tamano];

            System.Diagnostics.Debug.WriteLine("Estoy en respuesta");
            for (int i = 0; i < tamano; i++)
                for (int j = 0; j < tamano; j++)
                {
                    
                    respuesta[i, j] = m1[i, j] + m2[i, j];
                    System.Diagnostics.Debug.WriteLine(" " + m1[i,j] + " + " + m2[i,j] + " = " + respuesta[i,j]);
                }
                    return respuesta;
        }

        private float[,] restar(float[,] m1, float[,] m2)
        {
            float[,] respuesta = new float[tamano, tamano];

            for (int i = 0; i < tamano; i++)
                for (int j = 0; j < tamano; j++)
                    respuesta[i, j] = m1[i, j] - m2[i, j];

            return respuesta;
        }

        private float[,] multiplicar(float[,] m1, float[,] m2)
        {
            float[,] respuesta = new float[tamano, tamano];
            float suma = 0;

            for (int i = 0; i < tamano; i++)
            {
                for (int j = 0; j < tamano; j++)
                {
                    suma = 0;
                    for (int k = 0; k < tamano; k++)
                    {
                        suma += m1[i,k] * m2[k,j];
                    }
                    respuesta[i,j] = suma;
                }
            }  

            return respuesta;
        }

    }

}
