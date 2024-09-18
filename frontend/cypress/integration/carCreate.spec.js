describe('Car Create Form Tests', () => {
    beforeEach(() => {
      cy.visit('/dashboard'); 
    });
  
    it('should add a new car successfully', () => {
    
      cy.intercept('GET', '/api/car/makes', { fixture: 'carMakes.json' }).as('getMakes');
      cy.intercept('GET', '/api/car/models', { fixture: 'carModels.json' }).as('getModels');
      cy.intercept('POST', '/api/car', { statusCode: 200, body: { id: 1, ...carData } }).as('createCar');
  
      cy.wait('@getMakes');
      cy.wait('@getModels');
  
  
      cy.get('#make').select('Toyota'); 
      cy.get('#model').select('Corolla'); 
      cy.get('#year').type('2024');
      cy.get('#vin').type('1234567890');
  
 
      cy.get('button[type="submit"]').click();
  
      cy.get('.alert-success').should('be.visible');
      cy.get('.alert-success').contains('Car added successfully');
    });
  
    it('should display error message for missing fields', () => {

      cy.get('#make').select(''); 
      cy.get('#model').select(''); 
      cy.get('#year').type(''); 
      cy.get('#vin').type('');
  
      cy.get('button[type="submit"]').click();
  
      cy.get('.alert-danger').should('be.visible');
      cy.get('.alert-danger').contains('Please fill out all required fields');
    });
  });
  