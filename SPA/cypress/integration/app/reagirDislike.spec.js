/// <reference types="cypress" />

//import Chance from 'chance';
//const chance = new Chance();

describe('comentarPost', ()=>{

  //gerar email aleatório
  //const email = chance.email();
  //const password = 'PasswordValida';

  it('login',()=>{
    cy.visit('http://localhost:4200/login')
    cy.location('pathname').should('eq','/login')

    //encontrar texto
    cy.contains('Login');

    cy.get('#emailLogin').type('1190742@isep.ipp.pt')
    cy.get('#passwordLogin').type('IsepInformatica61*')
    cy.get('#botaoLogin').click()

    cy.go('forward')
    cy.location('pathname').should('include', '/inicio')
  })

  it('comentarPostDislike',()=>{
    cy.get('#botaoPost').click()
    cy.get('#botaoComentarPost').click()

    cy.go('forward')
    cy.location('pathname').should('include','/verFeed')

    cy.get('#emailFeed').type('1190742@isep.ipp.pt')
    cy.get('#buscarFeedPostsUser').click()
    cy.wait(2000)

    cy.get('#botaoPostarReacao').click()
    cy.wait(1000)

    cy.get('#botaoDislike').click()
    cy.wait(2000)
  })

})
