import React from "react";

class AddPerson extends React.Component {
  //creating a state directly in the class component 
  state = {
    firstName: "",
    lastName: "",
    age: ""
  };

  add = (e) => {
    e.preventDefault();  //stops the default action of an element (we are using a button and we dont want our page to get refreshed)
    if (this.state.firstName === "" || this.state.lastName === "" || this.state.age === "") {
      alert("All the fields are mandatory!");
      return;
    }
    if (this.state.age > 60 || this.state.age < 18) {
      alert("The age is not in the range of 18 to 60.");
      return;
    }
    if (this.state.firstName.length < 2) {
      alert("First name should be a minimum of 2 characters long.");
      return;
    }
    this.props.addPersonHandler(this.state); //we passed addPersonHandler from the App component and we pass our state here which contains firstName, lastName and age
    this.setState({ firstName: "", lastName: "", age: "" }); //clears the firstName, lastName and age fields after adding 
    this.props.history.push("/");
  };
  render() {
    return (
      <div className="ui main">
        <h2>New Person</h2>
        <form className="ui form" onSubmit={this.add}>
          <div className="field">
            <label>First Name</label>
            <input
              type="text"
              name="firstName"
              placeholder="First Name"
              value={this.state.firstName}
              onChange={(e) => this.setState({ firstName: e.target.value })}
            />
          </div>
          <div className="field">
            <label>Last Name</label>
            <input
              type="text"
              name="lastName"
              placeholder="Last Name"
              value={this.state.lastName}
              onChange={(e) => this.setState({ lastName: e.target.value })}
            />
          </div>
          <div className="field">
            <label>Age</label>
            <input
              type="text"
              name="age"
              placeholder="Age"
              value={this.state.age}
              onChange={(e) => this.setState({ age: e.target.value })}
            />
          </div>
          <button className="ui orange button">Add</button>
        </form>
      </div>
    );
  }
}

export default AddPerson;
