import React, { useContext, useState } from "react";
import { Dropdown, Input, Menu } from "antd";
import TagsContext from "../Contexts/FilterTagsContext";

function TagInput({ tags, addTag, inputPlaceholder }) {

    const { getTags } = useContext(TagsContext);

    const [searchedText, setSearchedText] = useState('');

    function menuItemClick(key) {
        setSearchedText('');
        getTags('');
        addTag(key);
    }

    const menu = () => {
        let menuItems = tags.map((mit) => (<Menu.Item key={mit}>{mit}</Menu.Item>));
        if (searchedText && !tags.find(tag => tag === searchedText)) menuItems.unshift(<Menu.Item key={searchedText}>{searchedText}</Menu.Item>) 
        return (
            <Menu onClick={menuItemClick}>
                {menuItems}
            </Menu>
        )
    }

    return (
        <Dropdown overlay={menu} trigger={['click']}>
            <Input
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