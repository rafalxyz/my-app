describe('create product', () => {
  beforeEach(() => {
    cy.visit('/#/products/create');
  });

  it('should not sent invalid form', () => {
    cy.contains('Save').click();
    cy.contains('Please provide name');
    cy.contains('Please provide price');
    cy.contains('Please provide quantity');
    cy.contains('Please provide description');
  });
});
