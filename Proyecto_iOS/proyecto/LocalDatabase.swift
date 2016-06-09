

/* LocalDatabase: Guarda toda la informacion de la aplicacion en el directorio local del dispositivo */
import Foundation
struct PropertyKeyDB {
    static let usuarios = "usuarios"
}

@objc class LocalDatabase: NSObject, NSCoding {
    var usuarios:[User] = []
    
    init?(usuarios:[User])
    {
        self.usuarios = usuarios        
    }
    
    
    
    // MARK: NSCoding
    func encodeWithCoder(aCoder: NSCoder) {
        aCoder.encodeObject(usuarios, forKey: PropertyKeyDB.usuarios)
        
    }
    
    required convenience init?(coder aDecoder: NSCoder) {
        
        let usuarios = aDecoder.decodeObjectForKey(PropertyKeyDB.usuarios) as! [User]

        self.init(usuarios:usuarios)
    }
    
    // MARK: Archiving Paths
    static let DocumentsDirectory = NSFileManager().URLsForDirectory(.DocumentDirectory, inDomains: .UserDomainMask).first!
    static let ArchiveURL = DocumentsDirectory.URLByAppendingPathComponent("userData")
}

