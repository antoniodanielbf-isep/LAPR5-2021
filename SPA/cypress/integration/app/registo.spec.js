/// <reference types="cypress" />

//import Chance from 'chance';
//const chance = new Chance();

describe('registo', ()=>{

  //gerar email aleatório
  //const email = chance.email();
  //const password = 'PasswordValida';

  //antes de cada ação
  beforeEach(()=>{
    cy.visit('http://localhost:4200');
  })

  //encontrar texto
  it('titulo',()=>{
    cy.contains('Login');
  })


  it('registo',()=>{
    cy.visit('http://localhost:4200/registo')
    cy.location('pathname').should('eq','/registo')

    cy.get('#emailRegisto').type('teste@isep.ipp.pt')
    cy.get('#nomeRegisto').type('Teste')
    cy.get('#breveDescricaoRegisto').type('teste')
    cy.get('#numeroTelefoneRegisto').type('987654321')
    cy.get('#dataNacimentoRegisto').type('24/03/1999')
    cy.get('#perfilFacebookRegisto').type('https://www.facebook.com/emanuel.salvadinho')
    cy.get('#perfilLinkedinRegisto').type('https://www.linkedin.com/in/jo%C3%A3o-pereira-118bb2208/')
    cy.get('#urlImagemRegisto').type('https://media-exp1.licdn.com/dms/image/C4E03AQF8B3gbkGk7Jg/profile-displayphoto-shrink_400_400/0/1615420669819?e=1648080000&v=beta&t=ORaGos0XMuk12L5Fr0wO9KZ_o6fcapHIAGg71zNn2EA')
    cy.get('#cidadePaisRegisto').type('Porto')
    cy.get('#passwordRegisto').type('IsepInformatica61*')
    cy.get('#estadoEmocionalRegisto').click()
    cy.contains('Alegria').click()
    cy.get('#tagsRegisto').type('prolog')
    cy.get('#consentimentoRegisto').click()
    cy.get('#botaoRegisto').click()

    cy.wait(1000)
    cy.go('forward')
    cy.location('pathname').should('include', '/escolherUtilizadoresObjetivo')
  })

})
