using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DatabaseController : MonoBehaviour {
  private string conn, sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;
	// Use this for initialization
	void Start () {
        conn = "URI=file:" + Application.dataPath + "/WordFightDB.db"; //Path to database.
        //Deletvalue(6);
		//insertvalue("QUAY"); 
       // Updatevalue("a","w@gamil.com","1st",1);
        readers();

	}

    public void insertvalue(int correctCount,int incorrectCount,int isWin)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("insert into GameResults (correctCount,incorrectCount,isWin) values (\"{0}\",\"{1}\",\"{2}\")",correctCount,incorrectCount,isWin);// table name
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
			  Debug.Log("INSERT SUCCESS");
            dbconn.Close();
        }
    }

	  public void readers()
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            sqlQuery = "SELECT * " + "FROM GameResults";// table name
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                int correctCount = reader.GetInt32(0);
                int incorrectCount = reader.GetInt32(1);
                int isWin = reader.GetInt32(2);
                Debug.Log("value= " + isWin );
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}