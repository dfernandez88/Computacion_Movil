package com.dati.datichat;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.List;

public class Archivos extends AppCompatActivity {
    ListView list;
    ArrayAdapter<String> adp;
    ArrayList<String> arch;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_archivos);
        arch = new ArrayList<String>();
        this.llenar();

        list = (ListView)findViewById(R.id.archivos_lista);
        adp = new ArrayAdapter<String>(this,android.R.layout.simple_list_item_1,arch);
        list.setAdapter(adp);


    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_archivos, menu);
        return true;
    }
    public void llenar(){
        this.arch.add("Hola1");
        this.arch.add("Hola2");
        this.arch.add("Hola3");
        this.arch.add("Hola4");
        this.arch.add("Hola5");
        this.arch.add("Hola6");
    }

}
