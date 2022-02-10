import * as THREE from 'three';
import {cilindroData} from './dataDefault'
import {CylinderGeometry, Mesh, MeshBasicMaterial} from "three";

export default class Cilindro {
  public object: Mesh<CylinderGeometry, MeshBasicMaterial>;

  public idRelacao: string = "";
  public tagsAB: string = "";
  public tagsBA: string = "";
  public forcaLigacaoAB: string = "0";
  public forcaLigacaoBA: string = "0";
  public height: number=0;
  constructor(heightV: number, idRelacaoV: string, tagsABV: string,
              tagsBAV: string, forcaABV: string, forcaBAV: string) {
    this.height=heightV;
    this.idRelacao = idRelacaoV;
    this.tagsAB = tagsABV;
    this.tagsBA = tagsBAV;
    this.forcaLigacaoAB = forcaABV;
    this.forcaLigacaoBA = forcaBAV;


    let geometry = new THREE.CylinderGeometry(cilindroData.radiusTop, cilindroData.radiusBottom, heightV,
      cilindroData.radialSegments);
    //let texture = new THREE.TextureLoader().load(cilindroData.texture);
    let material = new THREE.MeshPhongMaterial({color: cilindroData.color});
    this.object = new THREE.Mesh(geometry, material);
  }

}
