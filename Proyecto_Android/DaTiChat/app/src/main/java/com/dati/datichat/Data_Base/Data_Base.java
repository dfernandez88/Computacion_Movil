package com.dati.datichat.Data_Base;

import android.database.sqlite.SQLiteOpenHelper;
import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.os.Build;
import android.provider.BaseColumns;
import com.dati.datichat.Adapters.ChatUser;
import com.dati.datichat.Data_Base.FormaDB.Usuario;

import static com.dati.datichat.Data_Base.FormaDB.*;

public class Data_Base extends SQLiteOpenHelper{

    private static final int DATABASE_VERSION = 1;
    private static final String DATABASE_NAME = "DatiDB.db";
    private final Context context;

    interface Tablas{
        String USER = "ChatUser";
        String SHARED_FILE = "ChatFiles";
        String MESSAGE = "ChatMessage";
    }

    interface Referencias {
        String ID_USUARIO = String.format("REFERENCES %s(%s)",Tablas.USER, Usuario.ID);
    }

    public Data_Base(Context context){
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
        this.context = context;

    }

    @Override
    public void onOpen(SQLiteDatabase db) {
        super.onOpen(db);
        if (!db.isReadOnly()) {
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT) {
                db.setForeignKeyConstraintsEnabled(true);
            } else {
                db.execSQL("PRAGMA foreign_keys=ON");
            }
        }
    }
    @Override
    public void onCreate(SQLiteDatabase db){
        db.execSQL(String.format("CREATE TABLE %s (%s INTEGER PRIMARY KEY," +
                            "%s STRING NOT NULL, %s STRING NOT NULL)",
                Tablas.USER,
                Usuario.ID, Usuario.USER_NAME,
                Usuario.NAME));
        db.execSQL(String.format("CREATE TABLE %s (%s INTEGER PRIMARY KEY," +
                            "%s INTEGER %s, %s INTEGER %s, %s TEXT, %s DATE)",
                Tablas.MESSAGE,
                Mensaje.ID, Mensaje.USER_FROM,
                Referencias.ID_USUARIO, Mensaje.USER_TO,
                Referencias.ID_USUARIO, Mensaje.MESSAGE_TEXT, Mensaje.DATE));
        db.execSQL(String.format("CREATE TABLE %s (%s INTEGER PRIMARY KEY," +
                            "%s BYTE, %s STRING, %s STRING, %s INTEGER %s, " +
                            "%s INTEGER %s, %s DATE)",
                Tablas.SHARED_FILE,
                Archivo.ID, Archivo.DATA_LOB,
                Archivo.NAME, Archivo.CONTENT_TYPE,
                Archivo.USER_FROM, Referencias.ID_USUARIO,
                Archivo.USER_TO, Referencias.ID_USUARIO,
                Archivo.DATE));
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        db.execSQL("DROP TABLE IF EXISTS " + Tablas.USER);
        db.execSQL("DROP TABLE IF EXISTS " + Tablas.MESSAGE);
        db.execSQL("DROP TABLE IF EXISTS " + Tablas.SHARED_FILE);

        onCreate(db);
    }
}
