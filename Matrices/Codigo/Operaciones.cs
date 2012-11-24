using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices.Codigo
{
    class Operaciones
    {

        float[,] matriz;

        public static float[,] multiplicacionPorEscalar(float escalar, float[,] matriz, int tamano)
        {
            float[,] resultado=new float[tamano,tamano];

            for(int i=0;i<tamano;i++)
                for(int j=0;j<tamano;j++)
                    matriz[i,j]*=escalar;

            return resultado;
        }//Fin de multiplicacion por esclar

    

        public static float[,] traspuesta(float[,] matriz,int tamano)
        {
            
            float[,] respuesta = new float[tamano, tamano];
            for (int i = 0; i < tamano; i++)
                for (int j = 0; j < tamano; j++)
                {
                    respuesta[i, j] = matriz[j, i];
                }
            return respuesta;
        }//Fin de traspuesta

        public static float traza(float[,] matriz,int tamano)
        {

            float respuesta = 0;

            for (int i = 0; i < tamano; i++)
                for (int j = 0; j < tamano; j++)
                {
                    respuesta += matriz[i, i];
                }
            return respuesta;
        }//Fin de traspuesta
     
    }//Fin de clase
}
