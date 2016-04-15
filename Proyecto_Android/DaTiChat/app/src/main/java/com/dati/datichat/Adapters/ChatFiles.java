package com.dati.datichat.Adapters;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import org.w3c.dom.Text;

import java.util.Date;

@JsonIgnoreProperties(ignoreUnknown = true)
public class ChatFiles {
    private Integer id;
    private String name;
    private String contentType;
    private Integer from;
    private Integer to;
    private String date;
    private String dataLob;

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getContentType() {
        return contentType;
    }

    public void setContentType(String contentType) {
        this.contentType = contentType;
    }

    public Integer getFrom() {
        return from;
    }

    public void setFrom(Integer from) {
        this.from = from;
    }

    public Integer getTo() {
        return to;
    }

    public void setTo(Integer to) {
        this.to = to;
    }

    public String getDate() {
        return date;
    }

    public void setDate(String date) {
        this.date = date;
    }

    public String getDataLob(){
        return dataLob;
    }

    public void setDataLob(String dataLob1){
        this.dataLob =dataLob1;
    }
}
