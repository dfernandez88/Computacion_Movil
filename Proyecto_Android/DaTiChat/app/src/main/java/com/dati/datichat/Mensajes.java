package com.dati.datichat;

import android.annotation.TargetApi;
import android.content.Context;
import android.content.Intent;
import android.database.DataSetObserver;
import android.os.Build;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AbsListView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;

import com.dati.datichat.Adapters.ChatArrayAdapter;
import com.dati.datichat.Adapters.ChatMessage;
import com.dati.datichat.Adapters.ChatUser;
import com.dati.datichat.Data_Base.Data_Base;
import com.dati.datichat.Data_Base.Operaciones_DB;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;

public class Mensajes extends AppCompatActivity {

    private ChatArrayAdapter adp;
    private ListView list;
    private EditText chatText;
    private Button send;
    private Operaciones_DB db;
    private ChatUser user;
    private Integer from;
    private Integer to;


    Intent intent;
    private boolean side = false;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_mensajes);
        intent = getIntent();

        list = (ListView) findViewById(R.id.chat_lista);

        adp = new ChatArrayAdapter(getApplicationContext(), R.layout.mensaje,from,to,user);
        list.setAdapter(adp);

        chatText = (EditText) findViewById(R.id.chat_text);
        chatText.setOnKeyListener(new View.OnKeyListener() {
            public boolean onKey(View v, int keyCode, KeyEvent event) {
                if ((event.getAction() == KeyEvent.ACTION_DOWN) && (keyCode == KeyEvent.KEYCODE_ENTER)) {
                    String texto = chatText.getText().toString();
                    return sendChatMessage(texto);
                }
                return false;
            }
        });

        list.setTranscriptMode(AbsListView.TRANSCRIPT_MODE_ALWAYS_SCROLL);
        list.setAdapter(adp);

        adp.registerDataSetObserver(new DataSetObserver() {
            public void OnChanged(){
                super.onChanged();
                list.setSelection(adp.getCount() -1);

            }
        });
    }
    private boolean sendChatMessage(String texto){
        ChatMessage message = new ChatMessage();
        message.setText(texto);
        final Calendar calendar = Calendar.getInstance();
        calendar.setTimeInMillis(System.currentTimeMillis());
        Date date = calendar.getTime();
        message.setDate(date.toString());
        message.setTo(to);
        message.setFrom(from);
        db.addMessage(message);
        adp.add(message.getText());
        chatText.setText("");
        side = !side;
        return true;
    }
    @TargetApi(Build.VERSION_CODES.M)
    public void BotonChatClick(View view){
        send = (Button) findViewById(R.id.chat_boton);
        send.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v){
                chatText = (EditText) findViewById(R.id.chat_text);
                String texto = chatText.getText().toString();
                sendChatMessage(texto);
            }
        });
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_chat, menu);
        return true;
    }

    public void Archivos(MenuItem item){
        Intent intent = new Intent(this,Archivos.class);
        startActivityForResult(intent,1);
    }
}
