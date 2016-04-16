package com.dati.datichat.Rest_Service;

import android.os.AsyncTask;

import com.dati.datichat.Adapters.ChatFiles;
import com.fasterxml.jackson.databind.ObjectMapper;

import org.springframework.http.converter.StringHttpMessageConverter;
import org.springframework.web.client.RestTemplate;

import java.io.IOException;
import java.util.ArrayList;

public class RestGetFiles extends AsyncTask<Integer,Void,ArrayList<ChatFiles>> {

    @Override
    protected ArrayList<ChatFiles> doInBackground(Integer... params) {
        String url = "http://172.17.4.59:8191/rest/files/{fromUserId}/{toUserId}";
        ArrayList<ChatFiles> files = null;

        RestTemplate restTemplate = new RestTemplate();

        restTemplate.getMessageConverters().add(new StringHttpMessageConverter());

        String result = restTemplate.getForObject(url, String.class, params[0],params[1]);
        try {

            ObjectMapper mapper = new ObjectMapper();
            files = mapper.readValue(result, mapper.getTypeFactory().constructCollectionType(ArrayList.class,ChatFiles.class));


        } catch (IOException e) {
            e.printStackTrace();
        }

        return files;
    }
}