import React, { useContext, useEffect, useState } from "react";
import { Dropdown } from "antd";
import TagsContext from "../Contexts/FilterTagsContext";

function TagInput({ tags, addTag, inputPlaceholder }) {

    const { getTags } = useContext(TagsContext);

    const [searchedText, setSearchedText] = useState('');

    function menuItemClick(key) {
        const keyText = key.target.text;
        setSearchedText('');
        getTags('');
        addTag(keyText);
    }

    //const [items, setItems] = useState([])

    const menuItems = () => {
        let tagItems = [...tags];
        if (searchedText && !tags.find(tag => tag === searchedText)) tagItems.unshift(searchedText)
        const menuItems = tagItems.map((tag) => {
            return { key: tag, label: (
            <a key={tag} onClick={menuItemClick} >{tag}</a>
        )}});

        return menuItems;
    }

    // useEffect(() => {
    //     getTags('');
    //     let tagItems = [...tags];
    //     if (searchedText && !tags.find(tag => tag === searchedText)) tagItems.unshift(searchedText)
    //     setItems(tagItems.map((tag, index) => { key = index.toString(), label = (
    //         <span>{tag}</span>
    //     )}));
    // }, []);

    // const menu = () => {
    //     let menuItems = tags.map((mit) => (<Menu.Item key={mit}>{mit}</Menu.Item>));
    //     if (searchedText && !tags.find(tag => tag === searchedText)) menuItems.unshift(<Menu.Item key={searchedText}>{searchedText}</Menu.Item>) 
    //     return (
    //         <Menu onClick={menuItemClick}>
    //             {menuItems}
    //         </Menu>
    //     )
    // }

    return (
        <Dropdown menu={{ items: menuItems() }} trigger={['click']}>
            <input
                size="large"
                value={searchedText}
                className="m-1 input-group-text"
                placeholder={inputPlaceholder}
                onChange={(e) => {
                    setSearchedText(e.target.value);
                    getTags(e.target.value);
                }}
                style={{ minWidth: 200 }}  
            />
        </Dropdown>
    );
}

export default TagInput;