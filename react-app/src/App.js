import React, { useState, useEffect } from "react";
import './App.css';
import Chat from "./Chat/Chat";
import Tags from "./Chat/Tags";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import TagsContext from "./Contexts/FilterTagsContext";
import Requests from "./Services/Requests";

function App() {
  // Tags
  //// Searched tags
  const [tags, setTags] = useState([]);

  async function getTags(startWith) {
    const requests = new Requests();
    const tags = await requests.getTags(startWith);
    setTags(tags ?? []);
  }

  useEffect(() => {
    getTags("");
  }, []);

  //// Filter tags for chat
  const [selectedFilterTags, setSelectedFilterTags] = useState([]);

  //// Message tags for chat
  const [selectedMessageTags, setSelectedMessageTags] = useState([]);

  //// SignalR
  const [connection, setConnection] = useState(null);

  useEffect(() => {
    const connect = new HubConnectionBuilder()
        .withUrl(process.env.REACT_APP_API_HUB ? process.env.REACT_APP_API_HUB : '')
        .configureLogging(LogLevel.Information)
        .withAutomaticReconnect()
        .build();

    setConnection(connect);
  }, [])

  useEffect(() => {
    if (connection) {
        connection
            .start()
            .then(() => {
                connection.on("NewMessage", (message) => console.log(message));
                connection.on("NewTags", (tags) => console.log(tags));
            }).catch((error) => console.log(error))
    }
  }, [connection])

  // const sendMessage = async (message) => {
  //     if (connection) await connection.send("Send", message);
  // }

  return (
    <div className="d-flex">
      <div className="full-height-container col-md-4">
      <TagsContext.Provider value={{ getTags }}>
        <Tags tags={tags} selectedTags={selectedFilterTags} setSelectedTags={setSelectedFilterTags} inputPlaceholder="Search filter tag..."/>
        <Tags tags={tags} selectedTags={selectedMessageTags} setSelectedTags={setSelectedMessageTags} inputPlaceholder="Search message tag..."/>
      </TagsContext.Provider>
      </div>
      <div className="col-md-8">
        <Chat filterTags={selectedFilterTags} messageTags={selectedMessageTags}/>
      </div>
    </div>
  );
}

export default App;
