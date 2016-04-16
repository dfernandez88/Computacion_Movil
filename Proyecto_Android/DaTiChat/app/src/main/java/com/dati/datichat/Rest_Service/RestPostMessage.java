package com.dati.datichat.Rest_Service;

import android.os.AsyncTask;

import com.dati.datichat.Adapters.ChatMessage;

import org.springframework.http.converter.StringHttpMessageConverter;
import org.springframework.web.client.RestTemplate;

import java.util.ArrayList;

public class RestPostMessage extends AsyncTask<String , Void, ChatMessage> {
    @Override
    protected ChatMessage doInBackground(String... params) {
        String url = "http://172.17.4.59:8191/rest/messages";

        RestTemplate restTemplate = new RestTemplate();
        ChatMessage message = new ChatMessage();
        message.setFrom(Integer.parseInt(params[0]));
        message.setTo(Integer.parseInt(params[1]));
        message.setText(params[2]);

        restTemplate.getMessageConverters().add(new StringHttpMessageConverter());
        ChatMessage postResponse = restTemplate.postForObject(url,message,ChatMessage.class);

        return postResponse;
    }
}