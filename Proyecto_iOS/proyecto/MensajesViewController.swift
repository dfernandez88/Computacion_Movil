

import UIKit
import SwiftyJSON

class MensajesViewController:UIViewController, UITableViewDelegate, UITableViewDataSource {
    
    var fromId = " "
    var toId = " "
    var contacto = Contacto(nombre: " ", username: " ", id: " ", mensajes: [Mensaje](), archivos: [Mensaje]())
    var mensajes = [Mensaje]()
    let ws = WebService()
    
    @IBOutlet weak var tableView: UITableView!
    @IBOutlet weak var navigation: UINavigationBar!
    @IBOutlet weak var navItem: UINavigationItem!
    @IBOutlet weak var mensaje: UITextField!
    
    
    // Trae los mensajes del servidor
    override func viewDidLoad() {
        super.viewDidLoad()
        
        tableView.dataSource = self
        tableView.delegate = self
        
        getMessagesFromServer()
    }
    
    // Navega a la ventana anterior (Corresponde a la barra de arriba que tiene las cosas de navegacion)
    override func viewWillAppear(animated: Bool) {
        let backButton: UIBarButtonItem = UIBarButtonItem(title: "Back", style: .Plain, target: self, action: #selector(back))
        self.navItem.leftBarButtonItem = backButton;
        navItem.title = contacto?.nombre

        super.viewWillAppear(animated);
    }
    
    func back() {
        self.dismissViewControllerAnimated(true, completion: nil)
    }
    
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
    }
    
    ///// Table View /////
    func tableView(tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return mensajes.count
    }
    
    
    func tableView(tableView: UITableView, cellForRowAtIndexPath indexPath: NSIndexPath) -> UITableViewCell {
        var cell:UITableViewCell = self.tableView.dequeueReusableCellWithIdentifier("cell")! as UITableViewCell
        var mensaje = self.mensajes[indexPath.row].mensaje
        
        //Si el mensaje es de la otra persona usa cell
        if (self.mensajes[indexPath.row].from == fromId){
            //Cuando un mensaje es nuevo le pone *
            if (self.mensajes[indexPath.row].new){
                mensaje += " *"
            }
            cell.textLabel?.text = mensaje
        }//De lo contrario usa cell2
        else{
            cell = self.tableView.dequeueReusableCellWithIdentifier("cell2")! as UITableViewCell
            let msm = cell.viewWithTag(5) as! UILabel
            msm.text = mensaje

        }
        
        return cell
    }
    
    func tableView(tableView: UITableView, didSelectRowAtIndexPath indexPath: NSIndexPath) {
        
    }
    ///// Table View /////
    
    //Recibe el json del servidor y los vuelve objetos de tipo mensaje
    func procesarMensajes(json:JSON){
        print(json)
        for (_,c) in json {
            let mensaje = Mensaje(mensaje: c["text"].string!, id: String(c["id"].int!), date: c["date"].string!, new:true, from:fromId, to:toId)!
            mensaje.owner = fromId
            mensajes.append(mensaje)
        }
        dispatch_async(dispatch_get_main_queue(),{
            self.ws.receiverMessage("", from: self.toId, to: self.fromId, completionHandler:self.procesarMensajesPropios)
        });
    }
    
    //Ordena los mensajes por fecha, guarda los nuevos mensajes y vuelve a cargar la vista
    func procesarMensajesPropios(json:JSON){
        print(json)
        for (_,c) in json {
            let mensaje = Mensaje(mensaje: c["text"].string!, id: String(c["id"].int!), date: c["date"].string!, new:true, from:toId, to:fromId)!
            mensaje.owner =  toId
            mensajes.append(mensaje)
        }
        
        mensajes.sortInPlace({ $0.date < $1.date })
        dispatch_async(dispatch_get_main_queue(),{
            self.guardarNoExistentes(self.fromId)
            self.tableView.reloadData()
        });
    }
    
    //Cuando se envia un mensaje vuelve y los trae todos para actualizarce
    func mensajeEnviado(json:JSON){
        print(json)
        dispatch_async(dispatch_get_main_queue(),{
            self.getMessagesFromServer()
        });

    }
    
    //Cuando se da click en el boton Enviar se crea un diccionario con la informacion del mensaje, se emvia al rest y se limpia el textBox
    @IBAction func enviar(sender: AnyObject) {
        if mensaje.text! != ""{
            let dictionary = ["from": toId, "to": fromId, "text": mensaje.text!] as NSDictionary;
            ws.sendMessage(dictionary, from: fromId, to: toId, completionHandler:mensajeEnviado)
            mensaje.text! = ""
        }
        

    }
    
    //Cuando se da click en el boton actualizar se traen los mensajes del servidor
    @IBAction func actualizar(sender: AnyObject) {
        getMessagesFromServer()
    }
    
    // Trae los mensaejes del servidor y los manda a procesar, elimina todos los mensajes actuales (evitar fallos)
    func getMessagesFromServer () {
        mensajes.removeAll()
        ws.receiverMessage("", from: fromId, to: toId, completionHandler:procesarMensajes)
        
    }
    
    //Cuando se da click en files abre la ventana de archivos
    @IBAction func goToFiles(sender: AnyObject) {
        let vc = storyboard!.instantiateViewControllerWithIdentifier("archivos") as! ArchivosViewController
        vc.fromId = fromId
        vc.toId = toId
        vc.contacto = contacto
        self.showViewController(vc, sender: nil)
    }
    
    //Se buscan los mensajes traidos y si es nuevo se guarda en la Database, si no es nuevo se le quita la etiqueta de nuevo 
    func guardarNoExistentes(idBuscado:String){
        let db = NSKeyedUnarchiver.unarchiveObjectWithFile(LocalDatabase.ArchiveURL.path!) as! LocalDatabase;
        
            for u in db.usuarios {
                if u.id == toId{
                    print("usuario encontrado")
                    for c in u.contactos {
                        if c.id == idBuscado{
                            print("contacto encontrado")
                            print (c.mensajes)
                            for m in mensajes {
                                var existe = false
                                for ms in c.mensajes{
                                    if ms.date == m.date{
                                        ms.new = false
                                        existe = true
                                        break
                                    }
                                }
                                if !existe {
                                    print("agregando nuevos mensajes")
                                    m.new = true
                                    c.mensajes.append(m)
                                }else{
                                    m.new = false
                                }
                            }
                            
                            NSKeyedArchiver.archiveRootObject(db, toFile: LocalDatabase.ArchiveURL.path!)
                            mensajes = c.mensajes
                        }
                    }
                }
            }
            
        
    }
    
}

