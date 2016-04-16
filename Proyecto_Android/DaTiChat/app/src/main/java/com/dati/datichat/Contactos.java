package com.dati.datichat;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.ListView;
import android.widget.Toast;

import com.dati.datichat.Adapters.ChatArrayAdapter;
import com.dati.datichat.Adapters.UserArrayAdapter;
import com.dati.datichat.Data_Base.Operaciones_DB;

public class Contactos extends AppCompatActivity {

    private UserArrayAdapter adp;
    private ListView list;
    private Operaciones_DB db;
    Intent intent;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_contactos);

        intent = getIntent();
        list = (ListView) findViewById(R.id.lista_contactos);

        adp = new UserArrayAdapter(getApplicationContext(),R.layout.contacto);

        if (!adp.isEmpty()){
            list.setAdapter(adp);
        }
        else {
            Toast.makeText(this, "No hay contactos", Toast.LENGTH_SHORT).show();
        }


    }
}
