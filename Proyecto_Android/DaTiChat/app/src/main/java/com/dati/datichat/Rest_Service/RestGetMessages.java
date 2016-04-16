package com.dati.datichat.Rest_Service;

import android.os.AsyncTask;

import com.dati.datichat.Adapters.ChatMessage;
import com.fasterxml.jackson.databind.ObjectMapper;

import org.springframework.http.converter.StringHttpMessageConverter;
import org.springframework.web.client.RestTemplate;

import java.io.IOException;
import java.util.ArrayList;

public class RestGetMessages extends AsyncTask <Integer,Void,ArrayList<ChatMessage>> {

    @Override
    protected ArrayList<ChatMessage> doInBackground(Integer... params) {
        String url = "http://172.17.4.59:8191/rest/messages/{fromUserId}/{toUserId}";
        ArrayList<ChatMessage> messages = null;
        RestTemplate restTemplate = new RestTemplate();

        restTemplate.getMessageConverters().add(new StringHttpMessageConverter());
        String result = restTemplate.getForObject(url, String.class, params[0],params[1]);
        try {

            ObjectMapper mapper = new ObjectMapper();
            messages = mapper.readValue(result, mapper.getTypeFactory().constructCollectionType(ArrayList.class,ChatMessage.class));


        } catch (IOException e) {
            e.printStackTrace();
        }

        return messages;
    }
}