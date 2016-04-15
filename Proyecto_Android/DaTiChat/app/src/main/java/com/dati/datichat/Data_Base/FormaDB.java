package com.dati.datichat.Data_Base;


import java.util.UUID;

public class FormaDB {

    interface ColumnasUsuario{
        String ID = "id";
        String USER_NAME = "user_name";
        String NAME = "name";

    }

    interface ColumnasMensaje{
        String ID = "id";
        String USER_FROM = "user_from";
        String USER_TO = "user_to";
        String MESSAGE_TEXT = "message_text";
    }

    interface ColumnasArchivo{
        String ID = "id";
        String DATA_LOB = "data_lob";
        String NAME = "name";
        String CONTENT_TYPE = "content_type";
        String USER_TO = "user_to";
        String USER_FROM = "user_from";
        String DATE = "date";
    }

    public static class Usuario implements ColumnasUsuario{
        public static String generarIdUsuario(){
            return "US-" + UUID.randomUUID().toString();
        }
    }
    public static class Mensaje implements ColumnasMensaje{
        public static  String generarIdMensaje(){
            return "MS-" + UUID.randomUUID().toString();
        }
    }
    public static class Archivo implements ColumnasArchivo{
        public static String generarIdArchivo(){
            return "AR-" + UUID.randomUUID().toString();
        }
    }
    private FormaDB(){
    }
}
