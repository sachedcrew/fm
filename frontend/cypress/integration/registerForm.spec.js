describe('Registration Form Tests', () => {
    beforeEach(() => {
      cy.visit('/register');
    });
  
    it('should register successfully and display success message', () => {
      cy.get('#email').type('newuser@example.com');
      cy.get('#password').type('newpassword');
      cy.get('form').submit();
  
      cy.get('.alert-success').should('be.visible');
      cy.get('.alert-success').contains('Registration successful!');
    });
  
    it('should display error message on registration failure', () => {
      cy.get('#email').type('invalidemail');
      cy.get('#password').type('short');
      cy.get('form').submit();
  

      cy.get('.alert-danger').should('be.visible');
      cy.get('.alert-danger').contains('Invalid email or password');
    });
  });
  