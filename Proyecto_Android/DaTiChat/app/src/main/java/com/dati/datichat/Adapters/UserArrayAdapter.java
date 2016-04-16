package com.dati.datichat.Adapters;


import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
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

public class UserArrayAdapter extends ArrayAdapter<String> {

    private TextView contact;
    private ChatUser user;
    private Operaciones_DB datos;
    private LinearLayout layout;
    private List<String> UserList = new ArrayList<String>();

    public UserArrayAdapter(Context context, int textViewResourceId ) {
        super(context, textViewResourceId);
        this.user = new ChatUser();
        this.user = this.datos.userP();
        final ArrayList<String> d = this.datos.findUsers();
        for (int i = 0; i < d.size(); i++) {
            UserList.add(d.get(i));
        }
    }

    public int getCount(){
        return this.UserList.size();
    }

    public String getItem(int index){
        return this.UserList.get(index);
    }

    public View getView(int position, View ConvertView, ViewGroup parent){

        View v = ConvertView;
        if(v==null){

            LayoutInflater inflater = (LayoutInflater) this.getContext().getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            v =inflater.inflate(R.layout.contacto, parent,false);

        }

        layout = (LinearLayout)v.findViewById(R.id.contacto1);
        String contactobj = getItem(position);

        contact =(TextView)v.findViewById(R.id.contacto_solo);
        contact.setText(contactobj);

        return v;

    }
    /*
    public Bitmap decodeToBitmap(byte[] decodedByte) {
        return BitmapFactory.decodeByteArray(decodedByte, 0, decodedByte.length);
    }
    */
}
