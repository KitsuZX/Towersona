using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader
{
   public static void ReadCSV(string file)
   {
        //Getting path        
        string path = getLocalPath() + "/CSV Files/" + file;

        if (File.Exists(path))
        {
            //Reading file from location
            StreamReader strReader = new StreamReader(path);

            string data_String = strReader.ReadToEnd();

            strReader.Close();

            //Array de strings de las filas enteras
            string[] rows = data_String.Split(new char[] { '\n' });
            List<float> data = new List<float>();

            int numRows = 0, numCols = 0;
            int startingRow = 1;
            int startingCol = 1;

            for (/*ROWS*/int i = 1; i <= 4; i++)
            {           
                string[] row = rows[i].Split(new char[] { ';', '/'});

                //Saca los valores de las celdas de una fila
                for (/*COLS*/int j = startingCol; j <= 2; j++)
                {                
                    float f;
                    float.TryParse(row[j], out f);
                    data.Add(f);

                    //TODO: leer solo las partes que interesen
                }            
            }

            //TODO: hacer lo que se quiera con DATA
            Debug.Log(data);
            
        }
    }

    private static string getLocalPath()
    {
        #if UNITY_EDITOR
                return Application.dataPath;
        #elif UNITY_ANDROID
                return Application.persistentDataPath;// +fileName;
        #elif UNITY_IPHONE
                return GetiPhoneDocumentsPath();// +"/"+fileName;
        #else
                return Application.dataPath;// +"/"+ fileName;
        #endif
    }
}
