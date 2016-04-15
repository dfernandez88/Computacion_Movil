package com.dati.datichat.Adapters;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

@JsonIgnoreProperties(ignoreUnknown = true)
public class ChatUser {
    private Integer user_Id;
    private String userName;
    private String name;

    public Integer getUserId() {
        return user_Id;
    }

    public void setUserId(Integer userId) {
        this.user_Id = userId;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    public String getNombre() {
        return name;
    }

    public void setNombre(String nombre) {
        this.name = nombre;
    }
}
