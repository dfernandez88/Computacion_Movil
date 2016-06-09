

/* WebService: Se encarga de la conexion con el servicio Rest la ip es sacada de Info.plist */

import Foundation
import SwiftyJSON
import UIKit

@objc class WebService:NSObject{
    
    let ipLocal = NSBundle.mainBundle().objectForInfoDictionaryKey("ip_web") as! String
    let cookiesKey = "session-cookie"
    
    override init()
    {
        super.init()
    }
 
    //Conexion con el Servidor, recibe el resto del url, si es GET o POST, lo parametros para el POST y una funcion
    func connectToServer(url:String, method:String, parametros:String, callback: (JSON) -> ()){

        let postsEndpoint: String = ipLocal + url
        
        let request = NSMutableURLRequest(URL: NSURL(string: postsEndpoint)!)
        request.HTTPMethod = method
        let postString = parametros
                
        request.HTTPBody = postString.dataUsingEncoding(NSUTF8StringEncoding)
        
        let task = NSURLSession.sharedSession().dataTaskWithRequest(request) { data, response, error in

            guard error == nil && data != nil else {                                                          // check for fundamental networking error
                print("error=\(error)")
                return
            }
            
            if let httpStatus = response as? NSHTTPURLResponse where httpStatus.statusCode != 200 {           // check for http errors
                print("statusCode should be 200, but is \(httpStatus.statusCode)")
                //print("response = \(response)")
            }else{
                
                let responseString = NSString(data: data!, encoding: NSUTF8StringEncoding)
                print("responseString = \(responseString)")
                
                let json = JSON(data: data!)
                //let favoritos = json["favoritos"]
                callback(json)
            }
            
            
        }
        task.resume()
    }
    
    
    
    //Otra forma de realizar la conexion con el servidor 
    func connectToServerJSON(url:String, method:String, dictionary:NSDictionary, callback: (JSON) -> ()){
        let postsEndpoint: String = ipLocal + url
        
        
        
        let request = NSMutableURLRequest(URL: NSURL(string: postsEndpoint)!)
        request.HTTPMethod = method
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        request.addValue("application/json", forHTTPHeaderField: "Accept")
        
        let data = try! NSJSONSerialization.dataWithJSONObject(dictionary, options: [])
        request.HTTPBody = data
        
        
        let task = NSURLSession.sharedSession().dataTaskWithRequest(request) { data, response, error in
            
            
            
            guard error == nil && data != nil else {                                                          // check for fundamental networking error
                print("error=\(error)")
                return
            }
            
            if let httpStatus = response as? NSHTTPURLResponse where httpStatus.statusCode != 200 {           // check for http errors
                print("statusCode should be 200, but is \(httpStatus.statusCode)")
                //print("response = \(response)")
            }else{
                
                let responseString = NSString(data: data!, encoding: NSUTF8StringEncoding)
                print("responseString = \(responseString)")
                
                let json = JSON(data: data!)
                //let favoritos = json["favoritos"]
                callback(json)
            }
            
            
        }
        task.resume()
    }
    
    
    //Trae los contactos de un usuario, retorna un json
    func logUser(parametros:String, id:String, completionHandler: (JSON) -> ()){
        connectToServer("/contacts/" + id, method:"get", parametros: parametros){(json) in
                completionHandler(json)
        }
    }
    
    //Trae los mensajes de un contacto con otro, retorna un json
    func receiverMessage(parametros:String, from:String, to:String, completionHandler: (JSON) -> ()){
        connectToServer("/messages/" + from + "/" + to, method:"get", parametros: parametros){(json) in
            completionHandler(json)
        }
    }
    
    //Envia un mensaje a un contacto, retorna un json
    func sendMessage(parametros:NSDictionary, from:String, to:String, completionHandler: (JSON) -> ()){
        connectToServerJSON("/messages/", method:"POST", dictionary: parametros){(json) in
            completionHandler(json)
        }
    }
    
    //Trae los archivos compartidos por dos contactos
    func receiverFiles(parametros:String, from:String, to:String, completionHandler: (JSON) -> ()){
        connectToServer("/shared_files/" + from + "/" + to, method:"get", parametros: parametros){(json) in
            completionHandler(json)
        }
    }
        
}