describe('Car Edit Form Tests', () => {
    beforeEach(() => {
      cy.visit('/dashboard'); 
  

      cy.intercept('GET', '/api/car/1', { fixture: 'carDetails.json' }).as('getCarDetails');
      cy.intercept('PUT', '/api/car/1', { statusCode: 200, body: { id: 1, make: 'Honda', model: 'Civic', year: '2025', vin: '0987654321' } }).as('updateCar');
      cy.intercept('GET', '/api/car/1', { fixture: 'carDetails.json' }).as('getCar');
  
      cy.wait('@getCarDetails');
    });
  
    it('should update a car successfully', () => {
   
      cy.get('#make').clear().type('Honda');
      cy.get('#model').clear().type('Civic'); 
      cy.get('#year').clear().type('2025'); 
      cy.get('#vin').clear().type('0987654321'); 
  
      cy.get('button[type="submit"]').click();

      cy.get('.alert-success').should('be.visible');
      cy.get('.alert-success').contains('Car updated successfully');
    });
  
    it('should display error message for invalid data', () => {

      cy.get('#make').clear(); 
      cy.get('#model').clear(); 
      cy.get('#year').clear(); 
      cy.get('#vin').clear(); 

      cy.get('button[type="submit"]').click();
  
      cy.get('.alert-danger').should('be.visible');
      cy.get('.alert-danger').contains('Please fill out all required fields');
    });
  
    it('should navigate back to create car form', () => {
      cy.get('button.btn-secondary').click();
  
      cy.url().should('include', '/dashboard/create'); 
    });
  });
  
