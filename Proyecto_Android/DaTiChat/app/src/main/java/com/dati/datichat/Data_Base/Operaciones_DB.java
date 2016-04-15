package com.dati.datichat.Data_Base;


import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.inputmethodservice.Keyboard;

import com.dati.datichat.Adapters.ChatFiles;
import com.dati.datichat.Adapters.ChatMessage;
import com.dati.datichat.Adapters.ChatUser;
import com.dati.datichat.Data_Base.FormaDB.Mensaje;
import com.dati.datichat.Data_Base.FormaDB.Usuario;
import com.dati.datichat.Data_Base.FormaDB.Archivo;
import com.dati.datichat.Data_Base.Data_Base.Tablas;

import java.util.ArrayList;

public class Operaciones_DB {
    private static Data_Base data_base;
    private static Operaciones_DB instacia = new Operaciones_DB();

    private Operaciones_DB(){
    }

    public static Operaciones_DB obtenerInstancia(Context context){
        if (data_base == null){
            data_base = new Data_Base(context);
        }
        return instacia;
    }

    //Operaciones Mensajes
    public void addMessage(ChatMessage Message){
        ContentValues valores = new ContentValues();
        valores.put(Mensaje.USER_TO, Message.getTo());
        valores.put(Mensaje.USER_FROM,Message.getFrom());
        valores.put(Mensaje.MESSAGE_TEXT,Message.getText());

        SQLiteDatabase db = data_base.getWritableDatabase();
        db.insertOrThrow(Tablas.MESSAGE, null,valores);
        db.close();
    }
    public ArrayList<String> BuscarMensajes(String from, String to){
        String query = "Select * FROM " + Tablas.MESSAGE + " WHERE " + Mensaje.USER_FROM + " = ?" +
                Mensaje.USER_TO + "= ? Order by DATE DESC";
        SQLiteDatabase db = data_base.getWritableDatabase();
        Cursor cursor = db.rawQuery(query,new String[]{from,to});
        ArrayList<String> queryResult = new ArrayList<>();
        if (cursor.moveToFirst()){
            queryResult.add(cursor.getString(3));
            while (cursor.moveToNext()){
                queryResult.add(cursor.getString(3));
            }
            cursor.close();
        }
        db.close();
        return queryResult;
    }

    //Operaciones Usuarios
    public void addUser(ChatUser user){
        ContentValues valores = new ContentValues();
        valores.put(Usuario.NAME,user.getNombre());
        valores.put(Usuario.USER_NAME,user.getUserName());

        SQLiteDatabase db = data_base.getWritableDatabase();
        db.insertOrThrow(Tablas.USER, null,valores);
        db.close();
    }
    public ArrayList<String> findUsers(){
        String query = "Select NAME FROM "+ Tablas.USER;
        SQLiteDatabase db = data_base.getWritableDatabase();
        Cursor cursor = db.rawQuery(query, new String[]{});
        ArrayList<String> queryResult = new ArrayList<>();
        if (cursor.moveToFirst()){
            queryResult.add(cursor.getString(2));
            while (cursor.moveToNext()){
                queryResult.add(cursor.getString(2));
            }
            cursor.close();
        }
        db.close();
        return queryResult;
    }

    //Operaciones Archivos
    public void addFile(ChatFiles file){
        ContentValues valores = new ContentValues();
        valores.put(Archivo.DATA_LOB,file.getDataLob());
        valores.put(Archivo.NAME,file.getName());
        valores.put(Archivo.CONTENT_TYPE,file.getContentType());
        valores.put(Archivo.USER_FROM,file.getFrom());
        valores.put(Archivo.USER_TO,file.getTo());
        valores.put(Archivo.DATE,file.getDate());

        SQLiteDatabase db = data_base.getWritableDatabase();
        db.insertOrThrow(Tablas.SHARED_FILE, null,valores);
        db.close();
    }
    public ArrayList<String> findFilesName(String from, String to){
        String query = "Select NAME from " + Tablas.SHARED_FILE + " WHERE " + Archivo.USER_FROM +
                " = ? or " + Archivo.USER_FROM + " = ? and " + Archivo.USER_TO + " = ? " +
                Archivo.USER_TO  + " = ? Order by NAME ASC";
        SQLiteDatabase db = data_base.getWritableDatabase();
        Cursor cursor = db.rawQuery(query,new String[]{from, to, from, to});
        ArrayList<String> queryResult = new ArrayList<>();
        if (cursor.moveToFirst()){
            queryResult.add(cursor.getString(0));
            while (cursor.moveToNext()){
                queryResult.add(cursor.getString(0));
            }
            cursor.close();
        }
        db.close();
        return queryResult;
    }
    public ArrayList<String> findFilesDate(String from, String to){
        String query = "Select NAME from " + Tablas.SHARED_FILE + " WHERE " + Archivo.USER_FROM +
                " = ? or " + Archivo.USER_FROM + " = ? and " + Archivo.USER_TO + " = ? " +
                Archivo.USER_TO  + " = ? Order by date( DATE ) DESC";
        SQLiteDatabase db = data_base.getWritableDatabase();
        Cursor cursor = db.rawQuery(query,new String[]{from, to, from, to});
        ArrayList<String> queryResult = new ArrayList<>();
        if (cursor.moveToFirst()){
            queryResult.add(cursor.getString(0));
            while (cursor.moveToNext()){
                queryResult.add(cursor.getString(0));
            }
            cursor.close();
        }
        db.close();
        return queryResult;
    }


}
