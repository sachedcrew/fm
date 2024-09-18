describe('Route Create Form Tests', () => {
    beforeEach(() => {
      cy.visit('/dashboard');
  
      cy.intercept('GET', '/api/cars', { fixture: 'cars.json' }).as('getCars');
      cy.intercept('POST', '/api/routes', { statusCode: 201, body: { id: 1, carId: 1, startLocation: 'A', endLocation: 'B', startTime: '2024-01-01T10:00', endTime: '2024-01-01T12:00', distance: 100, fuelUsed: 10 } }).as('createRoute');
  
      cy.wait('@getCars');
    });
  
    it('should create a route successfully', () => {
      cy.get('#carId').select('1'); 
      cy.get('#startLocation').type('Start Location');
      cy.get('#endLocation').type('End Location');
      cy.get('#startTime').type('2024-01-01T10:00');
      cy.get('#endTime').type('2024-01-01T12:00');
      cy.get('#distance').type('100');
      cy.get('#fuelUsed').type('10');
  
      cy.get('button[type="submit"]').click();
  
      cy.get('.alert-success').should('be.visible');
      cy.get('.alert-success').contains('Route created successfully');
    });
  
    it('should display error message for invalid data', () => {
      cy.get('#carId').select(''); 
      cy.get('#startLocation').clear(); 
      cy.get('#endLocation').clear(); 
      cy.get('#startTime').clear(); 
      cy.get('#endTime').clear(); 
      cy.get('#distance').clear();
      cy.get('#fuelUsed').clear(); 
  
   
      cy.get('button[type="submit"]').click();
  
      cy.get('.alert-danger').should('be.visible');
      cy.get('.alert-danger').contains('Please fill out all required fields');
    });
  });
  