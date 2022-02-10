import {Component, HostListener, NgZone, OnInit} from "@angular/core";
import IRelacaoDTO from "../dto/relacao-dto/IRelacaoDTO";
import {CSS2DObject, CSS2DRenderer} from "three/examples/jsm/renderers/CSS2DRenderer";
import {Esfera} from "../../models/esfera";
import {Nivel} from "../../models/nivel";
import Cilindro from "../../models/cilindro";
import {RedeSocialService} from "../services/rede-social-service/RedeSocialService";
import {UtilizadorService} from "../services/utilizador-service/utilizador.service";
import {Router} from "@angular/router";
import * as THREE from "three";
import {ITamanhoRedeSocialTotalDTO} from "../dto/rede-social-dto/ITamanhoRedeSocialTotalDTO";
import IRedeSocialGetDTO from "../dto/rede-social-dto/IRedeSocialGetDTO";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ICaminhoDTO} from "../dto/rede-social-dto/ICaminhoDTO";
import IUtilizadorDTO from "../dto/utilizador-dto/IUtilizadorDTO";
import {ITamanhoDTO} from "../dto/rede-social-dto/ITamanhoDTO";
import {OrbitControls} from "three/examples/jsm/controls/OrbitControls";
import {CSS3DRenderer} from "three/examples/jsm/renderers/CSS3DRenderer";
import {esferaData, cilindroData} from 'src/models/dataDefault'
import {MatSnackBar} from "@angular/material/snack-bar";

interface INivel {
  nivel: string;
  number: string;
}

export enum KEY_CODE {
  RIGHT_ARROW = 68,//D
  LEFT_ARROW = 65,//A
  FORWARD_ARROW = 87,//W
  BACK_ARROW = 83,//S
  UP_ARROW = 80,//P
  DOWN_ARROW = 76,//L
  SPEED_SHIFT = 16,//Left Shift
}

@Component({
  selector: 'app-rede-social',
  templateUrl: './rede-social.component.html',
  styleUrls: ['./rede-social.component.css']
})
export class RedeSocialComponent implements OnInit {


  //-----GRAFO----//
  scene: THREE.Scene = new THREE.Scene();
  camera: THREE.PerspectiveCamera = new THREE.PerspectiveCamera();
  cameraMinimapa: THREE.PerspectiveCamera = new THREE.PerspectiveCamera();
  nivel: Nivel = new Nivel(0);

  renderer: THREE.WebGLRenderer = new THREE.WebGLRenderer(); //fazer new THREE.WebGLRenderer???
  labelRenderer: CSS3DRenderer = new CSS3DRenderer();
  allNiveis: Array<Nivel> = new Array<Nivel>();
  esferas: Array<Esfera> = new Array<Esfera>();
  cilindros: Array<Cilindro> = new Array<Cilindro>();
  labelsEsferas: CSS2DObject[] = new Array<CSS2DObject>();
  labelsAuxEsferas: CSS2DObject[] = new Array<CSS2DObject>();
  labelsCilindros: CSS2DObject[] = new Array<CSS2DObject>();
  followLight: THREE.DirectionalLight = new THREE.DirectionalLight(0xffffff, 1);
  pointer: THREE.Vector2 = new THREE.Vector2();
  raycaster: THREE.Raycaster = new THREE.Raycaster();

  checkOrbit: boolean = true;
  speedCheck: boolean = false;

  check2D: boolean = false;

  lookAtX: number = 0;
  lookAtY: number = 0;
  lookAtZ: number = 0;

  windowWidth: number = 0;
  windowHeight: number = 0;
  windowsResize: number = 0;
  controls: OrbitControls = new OrbitControls(this.camera, this.labelRenderer.domElement);


  public rede: Array<Array<IRelacaoDTO>> = new Array<Array<IRelacaoDTO>>();
  niveis: INivel[] = [
    {nivel: '2', number: '2'},
    {nivel: '3', number: '3'}
  ];

  opcaoEscolhida: IRedeSocialGetDTO = {
    nivel: 3,
    utilizadorDestino: '',
    valorMinimo: 0,
  }

  editarRede = new FormGroup({
    nivelEditado: new FormControl(''),
    utilizadorDestino: new FormControl('', Validators.required),
    valorMinimo: new FormControl('', Validators.required),
  });

  caminhoEncontrado: ICaminhoDTO = {
    caminho: [''], valor: 0
  }

  caminhoSeguro: boolean = false;
  redeAtiva: boolean = false;
  caminhoMaisCurtoEncontrado: boolean = false;
  caminhoMaisForteEncontrado: boolean = false;
  caminhoMaisSeguroEncontrado: boolean = false;


  fortalezaRedeCalculada: boolean = false;
  fortalezaRede: number = 0;

  tamanhoDaRede: ITamanhoDTO = {users: [''], tamanho: 0}
  tamanhoRedeCalculado: boolean = false;
  tamanhoRedeForm = new FormGroup({
    nivelEditado: new FormControl('', Validators.required)
  });
  totalUtilizadoresSistema: ITamanhoRedeSocialTotalDTO = {tamanhoRedeSocialCompleto: 0};
  utilizadores: Array<IUtilizadorDTO> = new Array<IUtilizadorDTO>();
  started: boolean = false;

  constructor(public redeSocialService: RedeSocialService,
              public utilizadorService: UtilizadorService,
              private ngZone: NgZone,
              private router: Router,
              private notification: MatSnackBar
  ) {

  }


  /**
   * ****************************INICIALIZAÇÃO*****************************************
   */

  ngOnInit(): void {
    this.scene.background = new THREE.Color(0x3b3b3b);
    this.getFromMDR();

  }

  ngAfterViewInit(): void {
    this.initialize();
    this.animate();
  }

  /**
   * ****************************MDR->CARREGAR DADOS*****************************************
   */
  private async getFromMDR() {
    await this.redeSocialService.getRedeSocialPerspetivaDTO(3).then(u => {
        if (u != undefined) {
          this.rede = u.rede;
          return true;
        }
        return false;
      }
    );
    await this.utilizadorService.getUtilizadores().then(u => {
        if (u != undefined) {
          this.utilizadores = u;
          return true;
        }
        return false;
      }
    );

    this.mostrarNotificacao("Rede Carregada com Sucesso",false);
  }

  /**
   * ****************************INICIAR ESTRUTURAS*****************************************
   */
  public iniciar(check3D: boolean): void {
    this.eliminarObjetos();
    this.criarNiveis();

    this.opcaoEscolhida.nivel = this.editarRede.value.nivelEditado;
    let primeiroCriado: boolean = false;
    let primeiraRelacao: boolean = false;
    let nivel = 1;
    for (let pos of this.rede.values()) {

      if (nivel < this.opcaoEscolhida.nivel) {
        for (let relacao of pos.values()) {

          //relação
          if (!primeiroCriado) {
            //criar esfera central
            this.criarEsfera(relacao.utilizadorOrigem, 0, check3D);
          }
          if (!primeiraRelacao) {
            this.criarEsfera(relacao.utilizadorDestino, nivel * 4, check3D);

            primeiraRelacao = true;
          }
          this.criarEsfera(relacao.utilizadorDestino, nivel * 4, check3D);
          //criar cilindro
          this.criarCilindro(relacao);

        }
      }
      nivel++;
    }

    this.criarLabels();

  }

  /**
   * ****************************CREATE THREE THINGS*****************************************
   */

  // SCENE
  private initialize(): void {
    this.scene = new THREE.Scene();
    this.scene.background = new THREE.Color(0x954c67);

    this.createMainView();
    this.createMiniMapView();
    this.addLigths();
    this.createRenderer();

    window.addEventListener('resize', this.windowResize.bind(this));


  }

  // VIEWS
  private createMainView(): void {
    this.camera = new THREE.PerspectiveCamera(30, window.innerWidth / window.innerHeight, 1, 5000);
    this.camera.position.z = 50;

    this.labelRenderer = new CSS2DRenderer();
    this.labelRenderer.setSize(window.innerWidth, window.innerHeight);
    this.labelRenderer.domElement.style.position = 'absolute';
    this.labelRenderer.domElement.style.top = '0px';
    document.body.appendChild(this.labelRenderer.domElement);

    this.controls = new OrbitControls(this.camera, this.labelRenderer.domElement);
  }

  private createMiniMapView(): void {
    this.cameraMinimapa = new THREE.PerspectiveCamera(10, window.innerWidth / window.innerHeight, 1, 5000);
    this.cameraMinimapa.lookAt(this.camera.position.clone().setY(0));
    this.cameraMinimapa.position.z = 100;
  }


  /**
   * ****************************OBJETOS*****************************************
   */
  //-----------------------------ESFERA-----------------------------------------
  private async criarEsfera(utilizadorOrigem: string, level: number, check3D: boolean) {

    let getUser: IUtilizadorDTO = this.getUserByEmail(utilizadorOrigem);

    let check: boolean = false;
    for (const pos of this.esferas) {
      if (utilizadorOrigem == pos.user.email) {
        check = true;
      }
    }

    if (!check) {
      let created: Esfera = new Esfera(getUser);
      //posicionamento esfera

      if (check3D) {
        let theta = Math.random() * Math.PI * 2;
        let v = Math.random();
        let phi = Math.acos((2 * v) - 1);
        let x = level * Math.sin(phi) * Math.cos(theta);
        let y = level * Math.sin(phi) * Math.sin(theta);
        let z = level * Math.cos(phi);
        created.object.position.set(x, y, z);
      } else {
        let theta = Math.random() * Math.PI * 2;
        let x = level * Math.cos(theta);
        let y = level * Math.sin(theta);
        created.object.position.set(x, y, 0);
      }
      this.esferas.push(created);
      this.scene.add(created.object);
    }
  }

  private getUserByEmail(utilizadorOrigem: string): IUtilizadorDTO {
    for (const pos of this.utilizadores) {
      if (utilizadorOrigem == pos.email) {
        return pos;
      }
    }
    throw new DOMException("not found")

  }

  //-----------------------------CILINDRO-----------------------------------------

  private criarCilindro(relacao: IRelacaoDTO) {
    //inserir cálculo altura
    let esferaOrigem;
    let esferaDestino;
    for (let pos of this.esferas) {
      if (pos.user.email == relacao.utilizadorOrigem) {
        esferaOrigem = pos.object;
      }
      if (pos.user.email == relacao.utilizadorDestino) {
        esferaDestino = pos.object;
      }

    }

    let diferencaX = esferaOrigem.position.x - esferaDestino.position.x;
    let diferencaY = esferaOrigem.position.y - esferaDestino.position.y;
    let diferencaZ = esferaOrigem.position.z - esferaDestino.position.z;


    let created: Cilindro = new Cilindro(
      this.distanciaEntreEsferas(diferencaX, diferencaY, diferencaZ),
      relacao.relacaoId, relacao.tagsRelacaoAB, relacao.tagsRelacaoBA, relacao.forcaLigacaoOrigDest,
      relacao.forcaLigacaoDestOrig);


    let distanceX = esferaOrigem.position.x - ((esferaOrigem.position.x - esferaDestino.position.x) / 2);
    let distanceY = esferaOrigem.position.y - ((esferaOrigem.position.y - esferaDestino.position.y) / 2);
    let distanceZ = esferaOrigem.position.z - ((esferaOrigem.position.z - esferaDestino.position.z) / 2);

    created.object.position.set(distanceX, distanceY, distanceZ);

    let angH = Math.atan2(diferencaX, diferencaZ);
    let angV = Math.acos((diferencaY) / this.distanciaEntreEsferas(diferencaX, diferencaY, diferencaZ));

    created.object.rotateY(angH);
    created.object.rotateX(angV);

    this.cilindros.push(created);
    this.scene.add(created.object);
  }

  //-----------------------------LABELS-----------------------------------------

  private criarLabels(): void {
    this.criarLabelsEsferas();
    this.criarLabelsCilindros();

    for (const lab of this.labelsAuxEsferas) {
      lab.visible = false;
    }
  }

  //ESFERAS -> USERS
  private criarLabelsEsferas(): void {
    for (const pos of this.esferas) {
      let esferaDiv = document.createElement('div');
      esferaDiv.className = 'labelEsfera';
      esferaDiv.textContent = pos.user.email;
      esferaDiv.style.color = "#ffff00";
      esferaDiv.style.marginTop = '-1em';
      esferaDiv.textContent.small();
      const esferaLabel = new CSS2DObject(esferaDiv);
      esferaLabel.position.set(0, 0.4, 0);
      pos.object.add(esferaLabel);
      this.labelsEsferas.push(esferaLabel);

      esferaDiv = document.createElement('div');
      esferaDiv.className = 'labelEsfera2';
      esferaDiv.textContent = pos.user.email + " / " + pos.user.nomeUtilizador + " / " + pos.user.dataDeNascimentoUtilizador +
        " / " + pos.user.breveDescricaoUtilizador;
      esferaDiv.style.color = "#ffff00";
      esferaDiv.style.marginTop = '-1em';
      esferaDiv.textContent.small();
      const esferaLabel2 = new CSS2DObject(esferaDiv);
      esferaLabel2.position.set(0, -0.4, 0);
      pos.object.add(esferaLabel2);
      this.labelsAuxEsferas.push(esferaLabel2);
    }
  }

  //CILINDROS -> RELAÇÕES
  private criarLabelsCilindros(): void {
    for (const pos of this.cilindros) {
      let cilindroDiv1 = document.createElement('div');
      cilindroDiv1.className = 'labelCilindroA';
      cilindroDiv1.textContent = pos.forcaLigacaoAB + "-" + pos.tagsAB;

      cilindroDiv1.style.marginTop = '-1em';

      cilindroDiv1.style.color = "#FF8C00";
      cilindroDiv1.textContent.small();

      let cilindroDiv2 = document.createElement('div');

      cilindroDiv2.className = 'labelCilindroB';
      cilindroDiv2.textContent = pos.forcaLigacaoBA + "-" + pos.tagsBA;

      cilindroDiv2.style.marginTop = '-1em';
      cilindroDiv2.style.color = "#7CFC00"
      cilindroDiv2.textContent.small();


      const cilindroLabel1 = new CSS2DObject(cilindroDiv1);
      const cilindroLabel2 = new CSS2DObject(cilindroDiv2);

      cilindroLabel1.position.set(0, 0.8, 0);
      cilindroLabel2.position.set(0, -0.8, 0);

      pos.object.add(cilindroLabel1);
      pos.object.add(cilindroLabel2);
      this.labelsCilindros.push(cilindroLabel1);
      this.labelsCilindros.push(cilindroLabel2);
    }
  }

  // RENDERER
  private createRenderer(): void {
    this.renderer = new THREE.WebGLRenderer({antialias: true});
    this.renderer.setPixelRatio(window.devicePixelRatio);
    this.renderer.setSize(window.innerWidth, window.innerHeight);
    this.renderer.autoClear = false;
    this.renderer.setClearColor(0x000000, 0.0);
    document.body.appendChild(this.renderer.domElement);
  }

  /**
   * ****************************ILUMINAÇÕES*****************************************
   */


  private addLigths(): void {
    const light = new THREE.AmbientLight(0x404040, 0.7); // soft white light
    this.scene.add(light);

    const directionalLight = new THREE.DirectionalLight(0xffffff, 0.125);

    directionalLight.position.x = 10;
    directionalLight.position.y = 10;
    directionalLight.position.z = 10;
    directionalLight.position.normalize();

    this.scene.add(directionalLight);

    const pointLight = new THREE.PointLight(0xffffff, 1);
    pointLight.position.x = 10;
    pointLight.position.y = 10;
    pointLight.position.z = 10;
    this.scene.add(pointLight);

    pointLight.add(new THREE.Mesh(new THREE.SphereGeometry(0.01, 8, 8),
      new THREE.MeshBasicMaterial({color: 0xffffff})));

    this.followLight = new THREE.DirectionalLight(0xffffff, 0.5);

    this.scene.add(this.followLight);
  }

  /**
   * ****************************HIGHLIGHT CAMINHOS*****************************************
   */
  private sinalizarCaminho(caminhoEncontrado: ICaminhoDTO): void {
    let relAux: Array<string> = new Array<string>()

    const caminho = caminhoEncontrado.caminho;


    for (let x = 0; x < caminho.length - 1; x++) {
      let relI = this.getIDRelacao(caminho[x], caminho[x + 1]);
      relAux.push(relI);
    }

    this.highLight(relAux);
  }

  private getIDRelacao(usrA: string, usrB: string): string {
    let bdRelacoes = this.rede;

    for (let nivel of bdRelacoes) {
      for (let relacao of nivel) {
        if (relacao.utilizadorOrigem == usrA && relacao.utilizadorDestino == usrB) {
          return relacao.relacaoId;
        }
      }

    }
    return "";
  }

  private highLight(relAux: Array<string>): void {
    for (let c of this.cilindros) {
      if (relAux.indexOf(c.idRelacao) != -1) {
        c.object.material.color.set(new THREE.Color(cilindroData.colorHighLight));
      }
    }
  }

  /**
   * ****************************ANIMAÇÕES*****************************************
   */

  private windowResize() {
    let camera = this.camera;
    camera.aspect = window.innerWidth / window.innerHeight;
    camera.updateProjectionMatrix();
    this.renderer.setSize(window.innerWidth, window.innerHeight);
    this.labelRenderer.setSize(window.innerWidth, window.innerHeight);
  }

  animate() {
    window.requestAnimationFrame(() => this.animate());

    if (this.checkOrbit) {
      this.controls.update();
    }

    this.render();
  }

  /**
   * ****************************RENDERIZAR*****************************************
   */

  private render() {
    if (this.windowWidth != window.innerWidth || this.windowHeight != window.innerHeight) {
      this.windowWidth = window.innerWidth;
      this.windowHeight = window.innerHeight;
      this.renderer.setSize(this.windowWidth, this.windowHeight);
    }

    let left = Math.floor(0);
    let bottom = Math.floor(0);
    let width = Math.floor(this.windowWidth);
    let height = Math.floor(this.windowHeight);

    this.renderer.setViewport(left, bottom, width, height);
    this.renderer.setScissor(left, bottom, width, height);
    this.renderer.setScissorTest(true);

    this.camera.aspect = width / height;

    this.followLight.position.copy(this.camera.position);

    this.renderer.render(this.scene, this.camera);
    this.labelRenderer.render(this.scene, this.camera);

    this.cameraMinimapa.lookAt(this.scene.position);

    left = Math.floor(this.windowWidth * 0.85);
    bottom = Math.floor(0);
    width = Math.floor(this.windowWidth * 0.15);
    height = Math.floor(this.windowHeight * 0.15);

    this.renderer.setViewport(left, bottom, width, height);
    this.renderer.setScissor(left, bottom, width, height);
    this.renderer.setScissorTest(true);
    //this.renderer.setClearColor(view.background);

    this.cameraMinimapa.aspect = width / height;
    this.cameraMinimapa.updateProjectionMatrix();

    this.renderer.render(this.scene, this.cameraMinimapa);
  }

  /**
   * ****************************NIVEIS*****************************************
   */

  private distanciaEntreEsferas(xA: number, yB: number, zC: number): number {
    return Math.sqrt(Math.pow(xA, 2) + Math.pow(yB, 2) + Math.pow(zC, 2));
  }

  private criarNiveis() {
    for (let i = 1; i < this.editarRede.value.nivelEditado; i++) {
      this.nivel = new Nivel(i);
      this.nivel.object.position.set(0, 0, 0);
      this.scene.add(this.nivel.object);
      this.allNiveis.push(this.nivel);
    }
  }

  /**
   * ****************************CAMINHOS*****************************************
   */

  consultarCaminhoMaisCurto() {

    this.resetCoresCaminhos();

    this.opcaoEscolhida.utilizadorDestino = this.editarRede.value.utilizadorDestino;
    this.redeSocialService.getCaminhoMaisCurto(this.opcaoEscolhida.utilizadorDestino).subscribe((caminhoMaisCurto) => {
      if (caminhoMaisCurto != null) {
        this.caminhoEncontrado = caminhoMaisCurto;
        this.caminhoMaisCurtoEncontrado = true;
        this.sinalizarCaminho(this.caminhoEncontrado);
      }
      if (caminhoMaisCurto == null) {
        ('CAMINHO VAZIO!');
      }
    });
  }

  consultarCaminhoMaisForte() {

    this.resetCoresCaminhos();

    this.opcaoEscolhida.utilizadorDestino = this.editarRede.value.utilizadorDestino;
    this.redeSocialService.getCaminhoMaisForte(this.opcaoEscolhida.utilizadorDestino).subscribe((caminhoMaisForte) => {
      if (caminhoMaisForte != null) {
        this.caminhoEncontrado = caminhoMaisForte;
        this.caminhoMaisForteEncontrado = true;
        this.sinalizarCaminho(this.caminhoEncontrado);
      }
      if (caminhoMaisForte == null) {
        ('CAMINHO VAZIO!');
      }
    });
  }

  consultarCaminhoMaisSeguro() {

    this.resetCoresCaminhos();

    this.opcaoEscolhida.utilizadorDestino = this.editarRede.value.utilizadorDestino;
    this.opcaoEscolhida.valorMinimo = this.editarRede.value.valorMinimo;
    this.redeSocialService.getCaminhoMaisSeguro(this.opcaoEscolhida.utilizadorDestino, this.opcaoEscolhida.valorMinimo)
      .subscribe((caminhoMaisSeguro) => {
        if (caminhoMaisSeguro != null) {
          this.caminhoEncontrado = caminhoMaisSeguro;
          this.caminhoMaisSeguroEncontrado = true;
          this.sinalizarCaminho(this.caminhoEncontrado);
        }
        if (caminhoMaisSeguro == null) {
          ('CAMINHO VAZIO!');
        }
      });
    this.caminhoSeguro = false;
  }


  public resetCoresCaminhos(): void {
    for (let c of this.cilindros) {
      c.object.material.color.set(new THREE.Color(cilindroData.color));
    }
  }


  /**
   * ****************************CONTROLOS*****************************************
   */

  //---------------------------ORBIT CONTROLS
  configControls() {
    this.controls.enableRotate = false;
    this.controls.enableZoom = false;
    this.controls.enablePan = false;
    this.usarTeclas();
    if (this.checkOrbit) {
      this.controls.update();
    }
  }

  //---------------------------RATO NOS OBJETOS

  AtivarRatoNaEsfera() {
    window.addEventListener('mousemove', (event => this.localClique(event)));
  }

  localClique(event: MouseEvent) {
    if (!this.check2D) {
      this.pointer.x = (event.clientX / window.innerWidth) * 2 - 1;
      this.pointer.y = -(event.clientY / window.innerHeight) * 2 + 1;

      this.raycaster.setFromCamera(this.pointer, this.camera);

      const intersects = this.raycaster.intersectObjects(this.scene.children, false);
      for (const lab of this.labelsAuxEsferas) {
        lab.visible = false;
      }

      if (intersects.length > 0) {
        for (const intersect of intersects) {
          for (const esf of this.esferas) {
            let text = esf.user.email + " / " + esf.user.nomeUtilizador + " / " + esf.user.dataDeNascimentoUtilizador +
              " / " + esf.user.breveDescricaoUtilizador;
            if (intersect.object.position == esf.object.position) {
              for (const lab of this.labelsAuxEsferas) {
                if (lab.element.textContent == text) {
                  lab.visible = true;
                }
              }
            }
          }
        }
      }
    }
  }


//---------------------------KEYBOARD

  usarTeclas() {
    window.addEventListener('keydown', event => this.keyEvent(event));
  }

  keyEvent(event: KeyboardEvent) {
    if (!this.checkOrbit) {
      console.log(event);

      if (event.keyCode === KEY_CODE.SPEED_SHIFT) {
        this.speedCheck = !this.speedCheck;
      }

      if (event.keyCode === KEY_CODE.RIGHT_ARROW) {
        if (this.speedCheck) {
          this.RightKey(5);
        } else {
          this.RightKey(1);
        }
      }

      if (event.keyCode === KEY_CODE.LEFT_ARROW) {
        if (this.speedCheck) {
          this.LeftKey(5);
        } else {
          this.LeftKey(1);
        }
      }

      if (event.keyCode === KEY_CODE.FORWARD_ARROW) {
        if (this.speedCheck) {
          this.ForwardKey(5);
        } else {
          this.ForwardKey(1);
        }
      }

      if (event.keyCode === KEY_CODE.BACK_ARROW) {
        if (this.speedCheck) {
          this.BackKey(5);
        } else {
          this.BackKey(1);
        }
      }

      if (event.keyCode === KEY_CODE.UP_ARROW) {
        if (this.speedCheck) {
          this.UpKey(1.5);
        } else {
          this.UpKey(0.3);
        }
      }

      if (event.keyCode === KEY_CODE.DOWN_ARROW) {
        if (this.speedCheck) {
          this.DownKey(1.5);
        } else {
          this.DownKey(0.3);
        }
      }
    }
  }

  private RightKey(speed: number) {
    if (!this.detetarColisao(this.camera.position.x + speed, this.camera.position.y, this.camera.position.z)) {
      this.camera.position.x += speed;
      this.lookAtX += speed;
      this.camera.lookAt(this.lookAtX, this.lookAtY, this.lookAtZ);
    }
  }

  private LeftKey(speed: number) {
    if (!this.detetarColisao(this.camera.position.x - speed, this.camera.position.y, this.camera.position.z)) {
      this.camera.position.x -= speed;
      this.lookAtX -= speed;
      this.camera.lookAt(this.lookAtX, this.lookAtY, this.lookAtZ);
    }
  }

  private ForwardKey(speed: number) {
    if (!this.detetarColisao(this.camera.position.x, this.camera.position.y, this.camera.position.z - speed)) {
      this.camera.position.z -= speed;
      this.lookAtZ -= speed;
      this.camera.lookAt(this.lookAtX, this.lookAtY, this.lookAtZ);
    }
  }

  private BackKey(speed: number) {
    if (!this.detetarColisao(this.camera.position.x, this.camera.position.y, this.camera.position.z + speed)) {
      this.camera.position.z += speed;
      this.lookAtZ += speed;
      this.camera.lookAt(this.lookAtX, this.lookAtY, this.lookAtZ);
    }
  }

  private UpKey(speed: number) {
    if (!this.detetarColisao(this.camera.position.x, this.camera.position.y + speed, this.camera.position.z)) {
      this.camera.position.y += speed;
      this.lookAtY += speed;
      this.camera.lookAt(this.lookAtX, this.lookAtY, this.lookAtZ);
    }
  }

  private DownKey(speed: number) {
    if (!this.detetarColisao(this.camera.position.x, this.camera.position.y - speed, this.camera.position.z)) {
      this.camera.position.y -= speed;
      this.lookAtY -= speed;
      this.camera.lookAt(this.lookAtX, this.lookAtY, this.lookAtZ);
    }
  }


  /**
   * ****************************2D/3D*****************************************
   */
  public iniciar2D() {
    this.lookAtX = 0;
    this.lookAtY = 0;
    this.lookAtZ = 0;
    this.camera.lookAt(this.lookAtX, this.lookAtY, this.lookAtZ);
    this.controls.enableRotate = false;
    this.controls.enableZoom = true;
    this.controls.enablePan = true;
    this.checkOrbit = true;
    this.controls.update();
    this.check2D = true;

    this.eliminarObjetos();
    //this.camera.lookAt(this.scene.position);
    this.camera.position.z = 50;
    this.started = false;
    this.iniciar(false);

  }

  public iniciar3D() {
    this.controls.enableRotate = false;
    this.controls.enableZoom = false;
    this.controls.enablePan = false;
    this.checkOrbit = false;
    this.check2D = false;

    // this.controls.update();
    this.AtivarRatoNaEsfera();
    this.usarTeclas();


    this.eliminarObjetos();
    //this.camera.lookAt(this.scene.position);
    this.camera.position.z = 50;
    this.started = false;
    this.iniciar(true);
  }

  public iniciar3DOrbit() {
    this.lookAtX = 0;
    this.lookAtY = 0;
    this.lookAtZ = 0;
    this.camera.lookAt(this.lookAtX, this.lookAtY, this.lookAtZ);
    this.controls.enableRotate = true;
    this.controls.enableZoom = true;
    this.controls.enablePan = true;
    this.checkOrbit = true;
    this.check2D = false;

    this.controls.update();
    this.AtivarRatoNaEsfera();
    //this.usarTeclas();


    this.eliminarObjetos();
    this.camera.lookAt(this.scene.position);
    this.camera.position.z = 50;
    this.started = false;
    this.iniciar(true);
  }


  eliminarObjetos() {
    for (let cil of this.cilindros) {
      for (const cilElement of this.labelsCilindros) {
        cil.object.remove(cilElement);
      }
      this.scene.remove(cil.object);
    }
    this.cilindros = new Array<Cilindro>();

    for (let esf of this.esferas) {
      for (const esfElement of this.labelsEsferas) {
        esf.object.remove(esfElement);
      }
      for (const esfElement of this.labelsAuxEsferas) {
        esf.object.remove(esfElement);
      }
      this.scene.remove(esf.object);
    }
    this.esferas = new Array<Esfera>();

    for (let niv of this.allNiveis) {
      this.scene.remove(niv.object);
    }
    this.allNiveis = new Array<Nivel>();

    this.labelsCilindros = new Array<CSS2DObject>();
    this.labelsEsferas = new Array<CSS2DObject>();
    this.labelsAuxEsferas = new Array<CSS2DObject>();
  }


  /**
   * ****************************DETEÇÃO COLISÕES*****************************************
   */

  private detetarColisao(x: number, y: number, z: number)
    :
    boolean {
    //this.checkColisaoCilindros() ||
    if (this.checkColisaoEsferas(x, y, z)) {
      window.alert('Colisão detetada! Reposicionando!');
      this.reposicionar();
      return true;
    }
    return false;
  }

  private checkColisaoCilindros()
    :
    boolean {
    let xCilindro, yCilindro, zCilindro, height: number;
    //percorrer os cilindros e ver se a posição da camara coincide com alguma deles
    for (let cil of this.cilindros) {
      xCilindro = cil.object.position.x;
      yCilindro = cil.object.position.y;
      zCilindro = cil.object.position.z;
      height = cil.height;

      if ((Math.pow(this.camera.position.x - xCilindro, 2)
          + Math.pow(this.camera.position.z - zCilindro, 2))
        <= Math.pow(cilindroData.radiusBottom, 2)
        && (yCilindro - height <= this.camera.position.y || this.camera.position.y <= yCilindro + height)) {
        return true;
      }
    }
    return false;
  }

  private checkColisaoEsferas(x: number, y: number, z: number)
    :
    boolean {
    //percorrer as esferas e ver se a posição da camara coincide com alguma delas

    for (let esf of this.esferas) {
      //(Xcam-Xesf)^2+(Ycam-Yesf)^2+(Zcam-Zesf)^2 <= r^2 equação da superfície esférica
      if ((Math.pow((x - esf.object.position.x), 2) + Math.pow((y - esf.object.position.y), 2)
        + Math.pow((z - esf.object.position.z), 2)) <= Math.pow(esferaData.radius, 2) + 1.5) {
        return true;
      }
    }

    return false;
  }

  private reposicionar() {
    this.camera.position.z = 50;
  }

  /**
   * ****************************ROUTER*****************************************
   */

  irParaInicio() {
    this.eliminarObjetos();
    this.router.navigate(['/inicio']);
  }

  /**
   * ****************************ANGULAR*****************************************
   */

//FORMS

  mostrarFormValorMinimo() {
    this.caminhoSeguro = true;
  }

//BUTTONS

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
