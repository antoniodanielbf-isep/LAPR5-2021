import * as THREE from "three";
import {colorByHumor, esferaData} from './dataDefault'
import IUtilizadorDTO from "../app/dto/utilizador-dto/IUtilizadorDTO";
import {UtilizadorMap} from "../app/mappers/utilizador-mapper/UtilizadorMap";

export class Esfera {
  public object: any;
  public user: IUtilizadorDTO = UtilizadorMap.toHelperDTO();

  constructor(usr: IUtilizadorDTO) {
    this.user = usr;

    let geometry = new THREE.SphereGeometry(esferaData.radius, esferaData.widthSegments, esferaData.heightSegments);
    //let texture = new THREE.TextureLoader().load(esferaData.texture);
    let material;

    if (this.user.estadoEmocionalUtilizador == 0) {
      material = new THREE.MeshPhongMaterial({color: colorByHumor.Alegria});
    } else if (this.user.estadoEmocionalUtilizador == 1) {
      material = new THREE.MeshPhongMaterial({color: colorByHumor.Angustia});
    }else if (this.user.estadoEmocionalUtilizador == 2) {
      material = new THREE.MeshPhongMaterial({color: colorByHumor.Esperanca});
    } else if (this.user.estadoEmocionalUtilizador == 3) {
      material = new THREE.MeshPhongMaterial({color: colorByHumor.Medo});
    } else if (this.user.estadoEmocionalUtilizador == 4) {
      material = new THREE.MeshPhongMaterial({color: colorByHumor.Alivio});
    } else if (this.user.estadoEmocionalUtilizador == 5) {
      material = new THREE.MeshPhongMaterial({color: colorByHumor.Dececao});
    } else if (this.user.estadoEmocionalUtilizador == 6) {
      material = new THREE.MeshPhongMaterial({color: colorByHumor.Orgulho});
    } else if (this.user.estadoEmocionalUtilizador == 7) {
      material = new THREE.MeshPhongMaterial({color: colorByHumor.Remorsos});
    } else if (this.user.estadoEmocionalUtilizador == 8) {
      material = new THREE.MeshPhongMaterial({color: colorByHumor.Gratidao});
    } else {
      material = new THREE.MeshPhongMaterial({color: colorByHumor.Raiva});
    }

    this.object = new THREE.Mesh(geometry, material);
  }
}
