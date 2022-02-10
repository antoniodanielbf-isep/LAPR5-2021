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

    cy.get('#emailLogin').type('1190402@isep.ipp.pt')
    cy.get('#passwordLogin').type('IsepInformatica61*')
    cy.get('#botaoLogin').click()

    cy.go('forward')
    cy.location('pathname').should('include', '/inicio')
  })

  it('fazerPost',()=>{
    cy.wait(2000)
    cy.get('#botaoPost').click()

    cy.get('#botaoCriarPost').click()

    cy.go('forward')
    cy.location('pathname').should('include','/criar-post')
    cy.wait(1000)

    cy.get('#textoPost').type('teste testado para comentar')
    cy.get('#tagsPost').type('teste, comentar')

    cy.get('#botaoCriarPost').click()

    cy.get('#botaoInicio').click()
    cy.go('forward')
    cy.location('pathname').should('include','/inicio')
  })


it('comentarPost',()=>{
    cy.get('#botaoPost').click()
    cy.get('#botaoComentarPost').click()

    cy.go('forward')
    cy.location('pathname').should('include','/verFeed')

    cy.get('#emailFeed').type('1190742@isep.ipp.pt')
    cy.get('#buscarFeedPostsUser').click()


    cy.get('#textoComentario').type('testePost')
    cy.get('#tagsComentario').type('teste, testado, fixe')

    cy.get('#botaoPostarComentario').click()
  })

})
