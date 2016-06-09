
/* Contacto: Un contacto tiene un id, un nombre, un usename, una lista de mensajes y una lista de archivos*/


import Foundation


struct PropertyKey {
    static let id = "id"
    static let nombre = "nombre"
    static let username = "username"
    static let mensajes = "mensajes"
    static let archivos = "archivos"
}

@objc class Contacto: NSObject, NSCoding {
    var id = ""
    var nombre = ""
    var username = ""
    var mensajes:[Mensaje] = []
    var archivos:[Mensaje] = []

    
    init?(nombre:String, username:String, id:String, mensajes:[Mensaje], archivos:[Mensaje])
    {
        self.id = id
        self.nombre = nombre;
        self.username = username;
        self.mensajes = mensajes;
        self.archivos = archivos;
        if username.isEmpty {
            return nil
        }
    }
    
    
    
    //Codificacion de un contacto en la Database
    func encodeWithCoder(aCoder: NSCoder) {
        aCoder.encodeObject(nombre, forKey: PropertyKey.nombre)
        aCoder.encodeObject(id, forKey: PropertyKey.id)
        aCoder.encodeObject(username, forKey: PropertyKey.username)
        
        aCoder.encodeObject(mensajes, forKey: PropertyKey.mensajes)
        aCoder.encodeObject(archivos, forKey: PropertyKey.archivos)

        
    }
    
    //Decodifica un contacto de la Database 
    required convenience init?(coder aDecoder: NSCoder) {
        
        let name = aDecoder.decodeObjectForKey(PropertyKey.nombre) as! String
        let id = aDecoder.decodeObjectForKey(PropertyKey.id) as! String
        let username = aDecoder.decodeObjectForKey(PropertyKey.username) as! String
        
        let mensajes = aDecoder.decodeObjectForKey(PropertyKey.mensajes) as! [Mensaje]
        let archivos = aDecoder.decodeObjectForKey(PropertyKey.archivos) as! [Mensaje]

        
        self.init(nombre:name, username:username, id:id, mensajes:mensajes, archivos:archivos)
    }
    
    
}

