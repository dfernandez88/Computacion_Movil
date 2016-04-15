package com.dati.datichat;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;

import com.dati.datichat.Adapters.ChatUser;

public class Registro extends AppCompatActivity {
    private ChatUser userApp;
    private Button registButton;
    private EditText user;
    private  EditText name;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registro);
    }

    public void registrar(){

    }
}
