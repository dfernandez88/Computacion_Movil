package com.dati.datichat.Adapters;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.dati.datichat.Data_Base.Operaciones_DB;
import com.dati.datichat.R;

import java.util.ArrayList;
import java.util.List;


public class ChatArrayAdapter extends ArrayAdapter<String> {
    private TextView chatText;
    private List<String> MessageList = new ArrayList<String>();
    private LinearLayout layout;
    private Operaciones_DB datos;

    private ChatUser user;
    private Integer from;
    private Integer to;


    public ChatArrayAdapter(Context context, int textViewResourceId,Integer from, Integer to, ChatUser user1) {
        super(context, textViewResourceId);
        this.user = user1;
        final ArrayList<String> d = datos.BuscarMensajes(from.toString(), to.toString());
        for (int i = 0; i < d.size(); i++) {
            MessageList.add(d.get(i));
        }
    }

    public void add(String object) {
        ChatMessage mensaje11 = new ChatMessage();
        mensaje11.setText(object);
        MessageList.add(mensaje11.getText());
        super.add(object);
    }

    public int getCount()
    {
        return this.MessageList.size();
    }

    public String getItem(int index){
        return this.MessageList.get(index);
    }

    public View getView(int position, View ConvertView, ViewGroup parent){

        View v = ConvertView;
        if(v==null){

            LayoutInflater inflater = (LayoutInflater) this.getContext().getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            v =inflater.inflate(R.layout.mensaje, parent,false);

        }

        layout = (LinearLayout)v.findViewById(R.id.message1);

        String messageobj = getItem(position);

        chatText =(TextView)v.findViewById(R.id.single_message);
        chatText.setText(messageobj);

        chatText.setBackgroundResource(from == this.user.getUserId() ? R.drawable.chat_a :R.drawable.chat_b);

        return v;

    }

    public Bitmap decodeToBitmap(byte[] decodedByte) {
        return BitmapFactory.decodeByteArray(decodedByte, 0, decodedByte.length);
    }

}
