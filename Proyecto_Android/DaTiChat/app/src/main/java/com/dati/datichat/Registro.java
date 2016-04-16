package com.dati.datichat;

import android.annotation.TargetApi;
import android.content.Intent;
import android.os.Build;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import com.dati.datichat.Adapters.ChatUser;
import com.dati.datichat.Data_Base.Operaciones_DB;

public class Registro extends AppCompatActivity {
    private ChatUser userApp;
    private Button registButton;
    private EditText user;
    private  EditText name;
    private Operaciones_DB db;
    private Button send;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registro);
    }


    public void Registrar(View view){
        send =  (Button) findViewById(R.id.boton_registro);
        send.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                //user = (EditText) findViewById(R.id.usuario11);
                //name = (EditText) findViewById(R.id.nombre11);
                //String texto1 = user.getText().toString();
                //String texto2 = name.getText().toString();
                //userApp.setUserName(texto1);
                //userApp.setNombre(texto2);
                //userApp.setUserId(1);
                   // db.addUser(userApp);

               // Continuar(v);
            }
        });
    }
    public void Continua2r(View view){
        Intent intent = new Intent(this,Contactos.class);
        startActivityForResult(intent, 1);
    }


}