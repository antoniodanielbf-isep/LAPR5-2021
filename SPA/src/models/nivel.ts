import * as THREE from "three";
import {esferaData} from './dataDefault'

export class Nivel {
  public object: any;
  public nivel: number = 0;


  constructor(nivelI: number) {
    this.nivel = nivelI;

    this.object = new THREE.Group();
    let geometry = new THREE.SphereGeometry(this.nivel * 20 * esferaData.radius, esferaData.widthSegments,
      esferaData.heightSegments);

    let material = new THREE.MeshPhongMaterial({
      color: 0x000000,
      side: THREE.DoubleSide,
      specular: 0xffffff,
      emissive: 0x000080,
      shininess: 10,
      opacity: 0.07,
      transparent: true
    });
    this.object.esfera = new THREE.Mesh(geometry, material);

    this.object.add(this.object.esfera);
  }
}
