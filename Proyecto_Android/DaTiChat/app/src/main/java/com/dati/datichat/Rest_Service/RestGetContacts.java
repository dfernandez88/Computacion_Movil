package com.dati.datichat.Rest_Service;

import android.os.AsyncTask;

import com.dati.datichat.Adapters.ChatUser;
import com.fasterxml.jackson.databind.ObjectMapper;

import org.springframework.http.converter.StringHttpMessageConverter;
import org.springframework.web.client.RestTemplate;

import java.io.IOException;
import java.util.ArrayList;

public class RestGetContacts extends AsyncTask<Integer,Void,ArrayList<ChatUser>> {
    @Override
    protected ArrayList<ChatUser> doInBackground(Integer... params) {
        String url = "http://172.17.4.59:8191/rest/contacts/{uId}";
        ArrayList<ChatUser> contactos = null;

        RestTemplate restTemplate = new RestTemplate();

        restTemplate.getMessageConverters().add(new StringHttpMessageConverter());

        String result = restTemplate.getForObject(url, String.class, params[0]);
        try {

            ObjectMapper mapper = new ObjectMapper();
            contactos = mapper.readValue(result, mapper.getTypeFactory().constructCollectionType(ArrayList.class,ChatUser.class));

        } catch (IOException e) {
            e.printStackTrace();
        }

        return contactos;
    }
}
