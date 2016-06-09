

/* ContactosViewController: Se encuntra enlazado a un list view y se encarga de mostrar los contactos de un usuario */

import UIKit
import SwiftyJSON



class ContactosViewController: UITableViewController{

    var user = User(contactos: [Contacto](),id: " ");
    var contactos = [Contacto]()
    
    //Se cargan los contactos del Rest
    override func viewDidLoad() {
        super.viewDidLoad()
        
        let inset = UIEdgeInsetsMake(20, 0, 0, 0);
        self.tableView.contentInset = inset;
        
        let ws = WebService()
        ws.logUser("", id: user!.id, completionHandler:procesarContactos)
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
    }
    
    
    func back() {
        self.dismissViewControllerAnimated(true, completion: nil)
    }
    
    
    ////// Table View //////
    override func tableView(tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return contactos.count
    }
    
    override func tableView(tableView: UITableView, cellForRowAtIndexPath indexPath: NSIndexPath) -> UITableViewCell {
        let cell:UITableViewCell = self.tableView.dequeueReusableCellWithIdentifier("cell")! as UITableViewCell
        
        cell.textLabel?.text = self.contactos[indexPath.row].nombre
        
        return cell
    }
    
    override func tableView(tableView: UITableView, didSelectRowAtIndexPath indexPath: NSIndexPath) {
        let vc = storyboard!.instantiateViewControllerWithIdentifier("mensajes") as! MensajesViewController
        vc.fromId = self.contactos[indexPath.row].id
        vc.toId = user!.id
        vc.contacto = procesarBD(self.contactos[indexPath.row])
        self.showViewController(vc, sender: nil)
    }
    ////// Table View //////
    
    
    //funcion que recibe el json y crea un contacto
    func procesarContactos(json:JSON){
        print(json)
        for (_,c) in json {
            contactos.append(Contacto(nombre: c["userName"].string!, username: c["nombre"].string!, id: String(c["userId"].int!), mensajes:[Mensaje](), archivos:[Mensaje]())!)
        }
        
        //dispatch_async(dispatch_get_main_queue(),{
            self.tableView.reloadData()
        //});
    }
    
    //Abre la base de datos y buscaun contacto, si lo encuentra lo retorna, si no entonces lo crea  
    func procesarBD(contacto:Contacto) -> Contacto{
        let db = NSKeyedUnarchiver.unarchiveObjectWithFile(LocalDatabase.ArchiveURL.path!) as? LocalDatabase;

        if db != nil{
            for u in db!.usuarios {
                if u.id == user!.id{
                    for c in u.contactos {
                        if c.id == contacto.id{
                            print("contacto encontrado")
                            return c
                        }
                    }
                }
            }
            user!.contactos.append(contacto)
            for u in db!.usuarios {
                if u.id == user!.id{
                    print("add user")
                    u.contactos.append(contacto)
                }
            }
            NSKeyedArchiver.archiveRootObject(db!, toFile: LocalDatabase.ArchiveURL.path!)
        }
        
        return contacto
    }
}

