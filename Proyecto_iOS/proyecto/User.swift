
/* User: un Usuario tiene un id y una lista de contactos */

import Foundation
struct PropertyKeyUser {
    static let id = "id"
    static let contactos = "contactos"
}

@objc class User: NSObject, NSCoding {
    var id = ""
    var contactos:[Contacto] = []
    
    init?(contactos:[Contacto], id:String)
    {
        self.id = id
        self.contactos = contactos;
        if id.isEmpty {
            return nil
        }
    }
    
    
    
    // Codidfica los usuarios en la DataBase
    func encodeWithCoder(aCoder: NSCoder) {
        aCoder.encodeObject(contactos, forKey: PropertyKeyUser.contactos)
        aCoder.encodeObject(id, forKey: PropertyKeyUser.id)
        
    }
    
    // Decodifica los usuarios en la Database 
    required convenience init?(coder aDecoder: NSCoder) {
        
        let name = aDecoder.decodeObjectForKey(PropertyKeyUser.contactos) as! [Contacto]
        let id = aDecoder.decodeObjectForKey(PropertyKeyUser.id) as! String
        
        
        self.init(contactos:name, id:id)
    }
}

