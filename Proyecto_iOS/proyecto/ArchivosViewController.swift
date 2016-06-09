


/* ArchivosViewController: Carga los archivos compartidos entre dos contactos, envia archivos y descarga archivos */
// La carga y descarga de archivos se hace utilizando la libreria Alamofire
import UIKit
import SwiftyJSON
import MobileCoreServices
import Alamofire

extension NSMutableData {
    
    
    func appendString(string: String) {
        let data = string.dataUsingEncoding(NSUTF8StringEncoding, allowLossyConversion: true)
        appendData(data!)
    }
}


class ArchivosViewController:UIViewController, UITableViewDelegate, UITableViewDataSource, UIImagePickerControllerDelegate, UINavigationControllerDelegate {
    
    let ipLocal = NSBundle.mainBundle().objectForInfoDictionaryKey("ip_web") as! String

    var fromId = " "
    var toId = " "
    var contacto = Contacto(nombre: " ", username: " ", id: " ", mensajes: [Mensaje](), archivos: [Mensaje]())
    var mensajes = [Mensaje]()
    let ws = WebService()
    
    @IBOutlet weak var tableView: UITableView!
    @IBOutlet weak var navigation: UINavigationBar!
    @IBOutlet weak var navItem: UINavigationItem!
    
    //Se cargan los archivos de Servidor
    override func viewDidLoad() {
        super.viewDidLoad()
        
        
        tableView.dataSource = self
        tableView.delegate = self
        
        self.mensajes.removeAll()
        self.ws.receiverFiles("", from: self.fromId, to: self.toId, completionHandler:self.procesarMensajes)
        
    }
    
    // Navega a la ventana anterior (Corresponde a la barra de arriba que tiene las cosas de navegacion)
    override func viewWillAppear(animated: Bool) {
        let backButton: UIBarButtonItem = UIBarButtonItem(title: "Back", style: .Plain, target: self, action: #selector(back))
        self.navItem.leftBarButtonItem = backButton;
        navItem.title = "Archivos"
        
        super.viewWillAppear(animated);
    }
    
    func back() {
        self.dismissViewControllerAnimated(true, completion: nil)
    }
    
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    
    ///// Table View /////
    func tableView(tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return mensajes.count
    }
    
    func tableView(tableView: UITableView, cellForRowAtIndexPath indexPath: NSIndexPath) -> UITableViewCell {
        let cell:UITableViewCell = self.tableView.dequeueReusableCellWithIdentifier("cell")! as UITableViewCell
        
        let nombre  = cell.viewWithTag(2) as! UILabel
        let fecha  = cell.viewWithTag(3) as! UILabel
        
        // Si es nuevo se le pone *
        var mensaje = self.mensajes[indexPath.row].mensaje
        if (self.mensajes[indexPath.row].new){
            mensaje += "  *"
        }
        nombre.text = mensaje
        fecha.text = self.mensajes[indexPath.row].date

        
        return cell
    }
    
    
    func tableView(tableView: UITableView, didSelectRowAtIndexPath indexPath: NSIndexPath) {
        let destination = Alamofire.Request.suggestedDownloadDestination(directory: .DocumentDirectory, domain: .UserDomainMask)
        
        print (destination)
        
        // Cuando se selcciona un elemento del Textview (archivo) se descarga
        var localPath: NSURL?
        Alamofire.download(.GET,
            ipLocal + "/files/"+self.mensajes[indexPath.row].id,
            destination: { (temporaryURL, response) in
                let directoryURL = NSFileManager.defaultManager().URLsForDirectory(.DocumentDirectory, inDomains: .UserDomainMask)[0]
                let pathComponent = response.suggestedFilename
                
                localPath = directoryURL.URLByAppendingPathComponent(pathComponent!)
                return localPath!
        })
            .response { (request, response, _, error) in
                print(response)
                print("Downloaded file to \(localPath!)")
                
        }
    }
    ///// Table View /////
    
    // Se traduce el Json en mensajes y se guardan los no existentes
    func procesarMensajes(json:JSON){
        print(json)
        for (_,c) in json {
            mensajes.append(Mensaje(mensaje: c["name"].string!, id: String(c["id"].int!), date: c["date"].string!, new:true, from:fromId, to:toId)!)
        }
        dispatch_async(dispatch_get_main_queue(),{
            self.guardarNoExistentes()
            self.tableView.reloadData()
        });
    }
    
    //Imprime el Json del envio
    func mensajeEnviado(json:JSON){
        print(json)
    }
    
    //Cuando se da click en enviar abre un menu para seleccionar lo que se desea enviar
    @IBAction func enviar(sender: AnyObject) {
        seleccionarFoto()
    }
    
    // Se actualizan los mensajes
    @IBAction func actualizar(sender: AnyObject) {
        self.mensajes.removeAll()
        self.ws.receiverFiles("", from: self.fromId, to: self.toId, completionHandler:self.procesarMensajes)
        
    }
    
    // Se recorren los mensajes y si exite un nuevo archivo se agrega a la Database
    func guardarNoExistentes(){
        let db = NSKeyedUnarchiver.unarchiveObjectWithFile(LocalDatabase.ArchiveURL.path!) as! LocalDatabase;
        
        for u in db.usuarios {
            if u.id == toId{
                print("usuario encontrado")
                for c in u.contactos {
                    if c.id == fromId{
                        print("contacto encontrado")
                        print (c.archivos)
                        for m in mensajes {
                            var existe = false
                            for ms in c.archivos{
                                if ms.id == m.id{
                                    existe = true
                                    break
                                }
                            }
                            if !existe {
                                print("agregando nuevos archivos")
                                c.archivos.append(m)
                                m.new = true
                            }else{
                                m.new = false
                            }
                        }
                        print (c.archivos)
                        NSKeyedArchiver.archiveRootObject(db, toFile: LocalDatabase.ArchiveURL.path!)
                        
                    }
                }
            }
        }
    }
    
    
    // Abre una alerta con las opciones a escojer (ojo la camara no funciona)
    func seleccionarFoto() {
        let imagePicker = UIImagePickerController()
        imagePicker.delegate = self
        imagePicker.mediaTypes = [kUTTypeImage as String]
        imagePicker.allowsEditing = false
        
        
        
        var refreshAlert = UIAlertController(title: "Subir", message: "Elige una opcion", preferredStyle: UIAlertControllerStyle.Alert)
        
        refreshAlert.addAction(UIAlertAction(title: "Camara", style: .Default, handler: { (action: UIAlertAction!) in
            imagePicker.sourceType = UIImagePickerControllerSourceType.Camera
            self.presentViewController(imagePicker, animated: true,
                completion: nil)
        }))
        refreshAlert.addAction(UIAlertAction(title: "Album", style: .Default, handler: { (action: UIAlertAction!) in
            imagePicker.sourceType = UIImagePickerControllerSourceType.PhotoLibrary
            self.presentViewController(imagePicker, animated: true,
                completion: nil)
            
        }))
        refreshAlert.addAction(UIAlertAction(title: "Cancel", style: .Default, handler: { (action: UIAlertAction!) in
            
        }))
        
        presentViewController(refreshAlert, animated: true, completion: nil)
        
    }
    
    // Cuando selecciona una imagen se envia
    func imagePickerController(picker: UIImagePickerController, didFinishPickingImage image: UIImage, editingInfo: [String : AnyObject]?) {
        self.dismissViewControllerAnimated(true, completion: nil)
        upload(image);
        
    }
    

    func generateBoundaryString() -> String {
        return "Boundary-\(NSUUID().UUIDString)"
    }
    
    
    // Envia un archivo al servidor
    func upload(image:UIImage){
        let imageData = UIImageJPEGRepresentation(image, 1)
        Alamofire.upload(
            .POST,
            ipLocal + "/files/"+fromId+"/"+toId,
            multipartFormData: { multipartFormData in
                if(imageData == nil){
                    
                }else{
                    multipartFormData.appendBodyPart(data: imageData!, name: "file", fileName: (self.randomStringWithLength(6) as String)+".jpg", mimeType: "image/jpg")
                }
            },
            encodingCompletion: { encodingResult in
                switch encodingResult {
                case .Success(let upload, _, _):
                    upload.responseJSON { response in
                        debugPrint(response)
                        dispatch_async(dispatch_get_main_queue(),{
                            self.mensajes.removeAll()
                            self.ws.receiverFiles("", from: self.fromId, to: self.toId, completionHandler:self.procesarMensajes)
                        });
                    }
                case .Failure(let encodingError):
                    print(encodingError)
                }
                
                

            }
        )
        
    }
    
    
    func imagePickerControllerDidCancel(picker: UIImagePickerController) {
        self.dismissViewControllerAnimated(true, completion: nil)
    }

    // Genera un nombre random para el archivo
    func randomStringWithLength (len : Int) -> NSString {
        
        let letters : NSString = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        
        var randomString : NSMutableString = NSMutableString(capacity: len)
        
        for (var i=0; i < len; i++){
            var length = UInt32 (letters.length)
            var rand = arc4random_uniform(length)
            randomString.appendFormat("%C", letters.characterAtIndex(Int(rand)))
        }
        
        return randomString
    }
    
}

