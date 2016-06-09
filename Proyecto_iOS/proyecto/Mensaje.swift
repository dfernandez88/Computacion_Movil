/* Mensaje: Un mensaje tiene un id, un contenido, una fecha, si es nuevo, un user_from y un user_to */

import Foundation
struct PropertyKeyMensaje {
    static let id = "id"
    static let mensaje = "mensaje"
    static let new = "new"
    static let date = "date"
    static let from = "from"
    static let to = "to"


}

@objc class Mensaje: NSObject, NSCoding {
    var id = ""
    var mensaje = ""
    var date = ""
    var new = true
    var owner = ""
    var from = ""
    var to = ""

    init?(mensaje:String, id:String, date:String, new:Bool, from:String, to:String )
    {
        self.id = id
        self.mensaje = mensaje;
        self.date = date;
        self.new = new
        self.from = from
        self.to = to

        
    }
    
    
    
    // Codofica los mensajes en la Database
    func encodeWithCoder(aCoder: NSCoder) {
        aCoder.encodeObject(mensaje, forKey: PropertyKeyMensaje.mensaje)
        aCoder.encodeObject(id, forKey: PropertyKeyMensaje.id)
        aCoder.encodeObject(date, forKey: PropertyKeyMensaje.date)
        aCoder.encodeObject(from, forKey: PropertyKeyMensaje.from)
        aCoder.encodeObject(to, forKey: PropertyKeyMensaje.to)

        aCoder.encodeObject(new, forKey: PropertyKeyMensaje.new)

        
    }
    
    //Decodifica los mensajes en la Database 
    required convenience init?(coder aDecoder: NSCoder) {
        
        let name = aDecoder.decodeObjectForKey(PropertyKeyMensaje.mensaje) as! String
        let id = aDecoder.decodeObjectForKey(PropertyKeyMensaje.id) as! String
        let date = aDecoder.decodeObjectForKey(PropertyKeyMensaje.date) as! String
        let from = aDecoder.decodeObjectForKey(PropertyKeyMensaje.from) as! String
        let to = aDecoder.decodeObjectForKey(PropertyKeyMensaje.to) as! String


        let new = aDecoder.decodeObjectForKey(PropertyKeyMensaje.new) as! Bool

        
        self.init(mensaje:name, id:id, date:date, new:new, from:from, to:to)
    }
}

