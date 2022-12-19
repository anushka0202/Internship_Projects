import React from "react";

class EditPerson extends React.Component {
  //when our edit component gets initialized, we need to change the state
  constructor(props) {
    super(props);  
    const { id, firstName, lastName, age } = props.location.state.person; //destructuring
    this.state = {
      id,         //means id: id (ES6 - key and the value are same)
      firstName,
      lastName,
      age
    };
  }

  update = (e) => {
    e.preventDefault();
    if (this.state.firstName === "" || this.state.lastName === ""|| this.state.age === "") {
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
    this.props.updatePersonHandler(this.state);
    this.setState({ firstName: "", lastName: "", age: "" });
    this.props.history.push("/");
  };
  render() {
    return (
      <div className="ui main">
        <h2>Edit Person</h2>
        <form className="ui form" onSubmit={this.update}>
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
          <button className="ui button orange">Update</button>
        </form>
      </div>
    );
  }
}

export default EditPerson;
