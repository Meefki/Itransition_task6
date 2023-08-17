import React, { useState, useEffect } from "react";
import './App.css';
import Chat from "./Chat/Chat";
import Tags from "./Chat/Tags";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import TagsContext from "./Contexts/FilterTagsContext";
import Requests from "./Services/Requests";
import FilterTagsContext from "./Contexts/FilterTagsContext";

function App() {
  // Messages
  const [messages, setMessages] = useState([]);

  const getMessages = async (tags = null) => {
    const requests = new Requests();
    const msgs = await requests.getMessages(tags);
    msgs.sort((m1, m2) => m1.sentDate > m2.sentDate ? 1 : m1.sentDate < m2.sentDate ? -1 : 0);
    setMessages(msgs ?? []);
  }

  useEffect(() => {
      getMessages();
  }, [])

  const [newMessage, setNewMessage] = useState();

  useEffect(() => {
    if (newMessage){
      let msgs = [...messages, newMessage];
      msgs.sort((m1, m2) =>  m1.sentDate > m2.sentDate ? 1 : m1.sentDate < m2.sentDate ? -1 : 0);
      if (selectedFilterTags && selectedFilterTags.some(el => {
        return !messages.tags || messages.tags.lenght == 0 || messages.tags.ifndexOf(el) !== -1;
      }))
      setMessages(msgs);
    }
  }, [newMessage])

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

  const updateFilter = (tags) => {
    setSelectedFilterTags(tags)
    getMessages(tags);
  }

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
      if (connection.state && connection.state !== "Disconnected")
        connection.stop();

      connection
          .start()
          .then(() => {
              connection.on("NewMessage", (message) => {
                setNewMessage(message);
              });
              connection.on("NewTags", () => getTags(''));
          }).catch((error) => console.log(error))
    }
  }, [connection])

  const sendMessage = async (message) => {
      if (connection) await connection.send("Send", message);
  }

  return (
    <div className="d-flex flex-column flex-md-row flex-lg-row flex-xl-row">
      <div className="full-height-container col-12 col-lg-4 col-md-6 col-sm-12 d-flex flex-column">
      <TagsContext.Provider value={{ getTags }}>
        <Tags tags={tags} selectedTags={selectedFilterTags} setSelectedTags={updateFilter} inputPlaceholder="Filter tags"/>
        <Tags tags={tags} selectedTags={selectedMessageTags} setSelectedTags={setSelectedMessageTags} inputPlaceholder="Message tags"/>
      </TagsContext.Provider>
      </div>
      <div className="col-12 col-lg-8 col-md-6 col-sm-12">
        <Chat messageTags={selectedMessageTags} sendMessage={sendMessage} messages={messages} />
      </div>
    </div>
  );
}

export default App;
