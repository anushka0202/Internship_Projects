import React, { useState, useEffect } from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import { uuid } from "uuidv4";
import api from "../api/people";
import "./App.css";
import Header from "./Header";
import AddPerson from "./AddPerson";
import PersonList from "./PersonList";
import PersonDetail from "./PersonDetail";
import EditPerson from "./EditPerson";


function App() {
  //const LOCAL_STORAGE_KEY = "people";
  const [people, setPeople] = useState([]);

  //Retrieve people
  const retrievePeople = async () => {         //async fn is used to wait for the promise. it makes the code wait untill the promise returns a result
    const response = await api.get("/people"); //URL from an API endpoint to get a promise which returns a response object
    return response.data;
  };

  //this is going to get a person from the AddPerson component
  const addPersonHandler = async (person) => {
    console.log(person);
    const request = {
      id: uuid(),   //creating an object with uuid
      ...person,    //for firstName, lastName and age
    };

    const response = await api.post("/people", request); // axios post call, second parameter is the request body which we want to send
    console.log(response);
    setPeople([...people, response.data]); //...people: previous state in the people array , response.data: add new person in the list
  };

  const updatePersonHandler = async (person) => {
    const response = await api.put(`/people/${person.id}`, person);
    const { id, firstName, lastName } = response.data;  //destructuring
    setPeople(
      people.map((person) => {   //map fn to manipulate the data in the array
        return person.id === id ? { ...response.data } : person;  //if person.id === id then we manipulate that object else return the old contact
      })
    );
  };

  const removePersonHandler = async (id) => {
    await api.delete(`/people/${id}`);  //when we delete we are not getting any response, we just get a status code
    const newPersonList = people.filter((person) => { //we create a copy of personList, once we get the copy we filter out the people
      return person.id !== id;
    });

    setPeople(newPersonList); //we set People to the new PersonList 
  };

  useEffect(() => {
    // const retrievePeople = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY)); //retrieving the data from local storage (after refresh)
    // if (retrievePeople) setPeople(retrievePeople);                              //setPeople only if retrievePeople is available
    const getAllPeople = async () => {
      const allPeople = await retrievePeople();  //now we are fetching the data from the server and not from the local storage
      if (allPeople) setPeople(allPeople);
    };

    getAllPeople();  //calling the function
  }, []);

  //for storing in local storage
  useEffect(() => {
    //localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(people));
  }, [people]); //[people] is the dependency

  return (
    <div className="ui container">
      <Router>
        <Header />
        <Switch>   {/* only finds the match and as soon as it finds a match it will not go and search for other routes */}
          <Route
            path="/"                //path specifies the url we want
            exact                   //matches the exact path
            render={(props) => (    // we use render when we want to pass props in our route
              <PersonList
                {...props}          //destructuring the props
                people={people} 
                getPersonId={removePersonHandler}
              />
            )}
          />
          <Route
            path="/add"
            render={(props) => (
              <AddPerson {...props} addPersonHandler={addPersonHandler} />
            )}
          />

          <Route
            path="/edit"
            render={(props) => (
              <EditPerson
                {...props}
                updatePersonHandler={updatePersonHandler}
              />
            )}
          />

          <Route path="/person/:id" component={PersonDetail} />
        </Switch>
      </Router>
    </div>
  );
}

export default App;
