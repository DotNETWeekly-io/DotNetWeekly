import React from 'react';
import { DotNetIcon } from './Icons';
import styles from './Header.module.css';

export class Header extends React.Component {
    render() {
        return (
            <div className={styles.banner}>
                <DotNetIcon />
                <p>.NET 周刊</p>
            </div>
        );
    }
}

export default Header;
