/// <reference types="cypress" />

//import Chance from 'chance';
//const chance = new Chance();

describe('login', ()=>{

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


  it('login',()=>{
    cy.visit('http://localhost:4200/login')
    cy.location('pathname').should('eq','/login')

    cy.get('#emailLogin').type('1190742@isep.ipp.pt')
    cy.get('#passwordLogin').type('IsepInformatica61*')
    cy.get('#botaoLogin').click()

    cy.go('forward')
    cy.location('pathname').should('include', '/inicio')
  })

})
